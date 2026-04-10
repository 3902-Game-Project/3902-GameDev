#nullable enable

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using GameProject.Interfaces;
using GameProject.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameProject.Managers;

public class LevelManager(Game1 game) : ILevelManager {
  private static readonly string[] LEVEL_NAMES = [
    "00_everything",
    "00b_confinement",
    "00c_non_confinement",
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
  private static readonly string STARTING_LEVEL = LEVEL_NAMES[0];

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

  public ILevel CurrentLevel => levels[currentLevelName];

  public void Initialize() { }

  public void LoadContent(ContentManager content) {
    if (LEVEL_NAMES.Length == 0) {
      throw new ArgumentException("There must be at least one level to load");
    }

    var levelNamesSet = new HashSet<string>(LEVEL_NAMES);

    foreach (var name in LEVEL_NAMES) {
      levels.Add(name, LevelLoader.FromString(game, levelNamesSet, File.ReadAllText(content.RootDirectory + "/Levels/" + name + ".csv")));
    }
  }

  public void Update(GameTime gameTime) {
    CurrentLevel.Update(gameTime);
  }

  public void Draw(GameTime gameTime) {
    CurrentLevel.Draw(gameTime);
  }

  public void SwitchLevel(string newLevelName) {
    if (!levels.ContainsKey(newLevelName)) {
      throw new ArgumentException($"level name unknown: '{newLevelName}'");
    }

    if (newLevelName != currentLevelName) {
      fadeToLevelName = newLevelName;
      // reloading StateGame to trigger level change code in the future
      game.ChangeState(game.StateGame);
    }
  }

  // Called by StateGameType.cs when fade out is complete
  public void CompleteLevelSwitch() {
    if (fadeToLevelName == null) {
      throw new InvalidConstraintException("CompleteLevelSwitch called when fadeToLevelName still null");
    }

    currentLevelName = fadeToLevelName;
    fadeToLevelName = null;
    game.StateGame.Player.Position = CurrentLevel.PlayerPosition;
    CurrentLevel.ProjectileManager.ClearProjectiles();
  }

  public void PreviousLevel() {
    SwitchLevelByIndex(Math.Max(CurrentLevelIndex - 1, 0));
  }

  public void NextLevel() {
    SwitchLevelByIndex(Math.Min(CurrentLevelIndex + 1, LEVEL_NAMES.Length - 1));
  }
}
