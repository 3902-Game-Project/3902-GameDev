#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using GameProject.Globals;
using GameProject.Items;
using GameProject.Level;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Managers;

internal class LevelManager(Game1 game) : ILevelManager {
  private static readonly string[] LEVEL_NAMES = [
    "00_everything",
    "00b_confinement",
    "00c_non_confinement",
    "00d_single_enemy",
    "01_level",
    "02_level",
    "03_level",
    "04_level",
    "05_level",
    "06_level",
    "07_level",
    "08_level",
    "09_level",
    "10_level",
    "11_level",
    "12_level",
    "13_level",
  ];
  private static readonly string STARTING_LEVEL = Flags.StartInDebugLevel ? "00_everything" : "01_level";
  public bool AllEnemiesCleared { get; set; } = true;
  public int PublicCurrentLevelIndex => CurrentLevelIndex;
  public static int TotalLevels => LEVEL_NAMES.Length;

  private readonly Dictionary<string, ILevel> levels = [];
  private string currentLevelName = STARTING_LEVEL;
  private string? fadeToLevelName = null;

  private int CurrentLevelIndex => Array.IndexOf(LEVEL_NAMES, currentLevelName);

  private void SwitchLevelByIndex(int newLevelIndex) {
    if (newLevelIndex < 0 || newLevelIndex >= LEVEL_NAMES.Length) {
      throw new ArgumentException("Attempt to set level index out of bounds");
    }

    SwitchLevel(LEVEL_NAMES[newLevelIndex]);
  }

  private void LevelSwitchUpdatePlayerPosition() {
    // Wrap player position around if player is at edge

    if (
      game.StateGame.Player.Position.X <= LevelLoader.PLAYER_LEFT_BOUNDARY_THRESHOLD
    ) {
      game.StateGame.Player.Position = new(
        LevelLoader.PLAYER_RIGHT_POS_AFTER_TELEPORT,
        game.StateGame.Player.Position.Y
      );
    } else if (
        game.StateGame.Player.Position.X >= LevelLoader.PLAYER_RIGHT_BOUNDARY_THRESHOLD
      ) {
      game.StateGame.Player.Position = new(
        LevelLoader.PLAYER_LEFT_POS_AFTER_TELEPORT,
        game.StateGame.Player.Position.Y
      );
    } else if (
      game.StateGame.Player.Position.Y <= LevelLoader.PLAYER_TOP_BOUNDARY_THRESHOLD
    ) {
      game.StateGame.Player.Position = new(
        game.StateGame.Player.Position.X,
        LevelLoader.PLAYER_BOTTOM_POS_AFTER_TELEPORT
      );
    } else if (
      game.StateGame.Player.Position.Y >= LevelLoader.PLAYER_BOTTOM_BOUNDARY_THRESHOLD
    ) {
      game.StateGame.Player.Position = new(
        game.StateGame.Player.Position.X,
        LevelLoader.PLAYER_TOP_POS_AFTER_TELEPORT
      );
    } else {
      // No wrapping possible, use default position
      game.StateGame.Player.Position = CurrentLevel.GetDefaultPlayerPosition();
    }
  }

  public ILevel CurrentLevel => levels[currentLevelName];

  public void Initialize() { }

  public void LoadContent(ContentManager content) {
    if (LEVEL_NAMES.Length == 0) {
      throw new ArgumentException("There must be at least one level to load");
    }

    if (CurrentLevelIndex == -1) {
      throw new ArgumentException("Starting level name not present in levels list");
    }

    var levelNamesSet = new HashSet<string>(LEVEL_NAMES);

    foreach (var name in LEVEL_NAMES) {
      var level = LevelLoader.FromString(game.StateGame.Player, game.StateGame.LevelManager, levelNamesSet, File.ReadAllText(content.RootDirectory + "/Levels/" + name + ".csv"));

      levels.Add(name, level);
    }

    // --- NEW DEBUG: SPAWN ON FLOOR IN STARTING LEVEL ---
    var factory = GameProject.Factories.ItemFactory.Instance;
    var player = game.StateGame.Player;

    // We add these directly to the "Starting Level" dictionary entry
    if (levels.TryGetValue(currentLevelName, out ILevel? startingLevel)) {
      // Positioned roughly in the middle of a standard room
      float testX = 200f;
      float testY = 300f;

      // Create the physical pickups you can walk over
      startingLevel.AddPickup(new ItemWorldPickup(factory.CreateSMG(testX, testY, player, this)));
      startingLevel.AddPickup(new ItemWorldPickup(factory.CreateFakeBFG(testX + 60, testY, player, this)));

      // Give the player ammo for the BFG so they can actually test it
      player.Inventory.Ammo[AmmoType.BFG] = 10;
      player.Inventory.Ammo[AmmoType.Light] = 100;
    }
    // --------------------------------------------------

    game.StateGame.Player.Position = CurrentLevel.GetDefaultPlayerPosition();
  }

  public void Update(double deltaTime) {
    CurrentLevel.Update(deltaTime);
  }

  public void Draw(SpriteBatch spriteBatch) {
    CurrentLevel.Draw(spriteBatch);
  }

  public void SwitchLevel(string newLevelName) {
    if (!levels.ContainsKey(newLevelName)) {
      throw new ArgumentException($"level name unknown: '{newLevelName}'");
    }

    if (newLevelName != currentLevelName) {
      // Check if we are leaving a level with enemies still alive
      if (CurrentLevel is GameProject.Level.Level lvl && lvl.HasKillableEnemiesRemaining) {
        AllEnemiesCleared = false;
      }

      fadeToLevelName = newLevelName;
      // reloading StateGame to trigger level change code in the future
      game.ChangeState(game.StateGame);
    }
  }

  // Called by StateGameType.cs when beginning to fade in stategame
  // If there is a level switch queued, process it now
  public void InitializeLevel() {
    if (fadeToLevelName != null) {
      currentLevelName = fadeToLevelName;
      fadeToLevelName = null;
      LevelSwitchUpdatePlayerPosition();
    }

    CurrentLevel.ProjectileManager.ClearProjectiles();
  }

  public void PreviousLevel() {
    SwitchLevelByIndex(Math.Max(CurrentLevelIndex - 1, 0));
  }

  public void NextLevel() {
    SwitchLevelByIndex(Math.Min(CurrentLevelIndex + 1, LEVEL_NAMES.Length - 1));
  }
}
