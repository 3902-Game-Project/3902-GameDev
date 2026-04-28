using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using GameProject.Enemies;
using GameProject.Factories;
using GameProject.GameStates;
using Microsoft.Xna.Framework;

namespace GameProject.SaveLoad;

internal static class SaveLoadManager {
  private const string SAVE_FILE_PATH = "savegame.json";

  public static void SaveGame(StateGameType stateGame) {
    var saveData = new GameSaveData();
    var player = stateGame.Player;
    var level = stateGame.LevelManager.CurrentLevel;
    var levelManager = stateGame.LevelManager;

    //Save Player State
    saveData.Player.X = player.Position.X;
    saveData.Player.Y = player.Position.Y;
    saveData.Player.Health = player.Health;
    saveData.Player.Ammo = new Dictionary<Items.AmmoType, int>(player.Inventory.Ammo);
    saveData.Player.ActiveWeaponIndex = player.Inventory.ActiveWeaponIndex;

    //Save weapons by their class name
    saveData.Player.WeaponTypes = player.Inventory.Weapons.Select(w => w.GetType().Name).ToList();

    //Save current level
    saveData.Level.LevelName = levelManager.CurrentLevelName;

    //Save Enemies State
    foreach (var enemy in level.GetAliveEnemies()) {
      saveData.Level.Enemies.Add(new EnemySaveData {
        TypeName = enemy.GetType().Name,
        X = enemy.Position.X,
        Y = enemy.Position.Y,
        Health = enemy.Health,
        Direction = enemy.Direction
      });
    }

    //Write to JSON
    string json = JsonSerializer.Serialize(saveData, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(SAVE_FILE_PATH, json);
  }

  public static void LoadGame(StateGameType stateGame) {
    if (!File.Exists(SAVE_FILE_PATH)) return;

    string json = File.ReadAllText(SAVE_FILE_PATH);
    var saveData = JsonSerializer.Deserialize<GameSaveData>(json);
    if (saveData == null) return;

    var player = stateGame.Player;
    var levelManager = stateGame.LevelManager;
    levelManager.ChangeLevel(saveData.Level.LevelName);

    //Load Player
    player.Position = new Vector2(saveData.Player.X, saveData.Player.Y);
    if (player.Shape is Collisions.Shapes.BoxCollider box) box.Position = player.Position;

    player.Health = saveData.Player.Health;
    player.Inventory.Ammo = new Dictionary<Items.AmmoType, int>(saveData.Player.Ammo);

    //Load Weapons
    player.Inventory.Weapons.Clear();
    foreach (var weaponName in saveData.Player.WeaponTypes) {
      switch (weaponName) {
        case "RevolverItem": player.Inventory.PickupItem(ItemFactory.Instance.CreateRevolver(0, 0, player, levelManager)); break;
        case "RifleItem": player.Inventory.PickupItem(ItemFactory.Instance.CreateRifle(0, 0, player, levelManager)); break;
        case "ShotgunItem": player.Inventory.PickupItem(ItemFactory.Instance.CreateShotgun(0, 0, player, levelManager)); break;
        case "SMGItem": player.Inventory.PickupItem(ItemFactory.Instance.CreateSMG(0, 0, player, levelManager)); break;
        case "BFGItem": player.Inventory.PickupItem(ItemFactory.Instance.CreateBFG(0, 0, player, levelManager)); break;
      }
    }
    player.Inventory.EquipWeapon(saveData.Player.ActiveWeaponIndex);

    //Load Enemies
    var restoredEnemies = new List<IEnemy>();
    foreach (var enemyData in saveData.Level.Enemies) {
      IEnemy newEnemy = enemyData.TypeName switch {
        "Snake" => EnemyFactory.Instance.CreateSnakeSprite(enemyData.X, enemyData.Y),
        "Bat" => EnemyFactory.Instance.CreateBatSprite(enemyData.X, enemyData.Y),
        "Shotgunner" => EnemyFactory.Instance.CreateShotgunnerSprite(enemyData.X, enemyData.Y, levelManager, player),
        "Rifleman" => EnemyFactory.Instance.CreateRiflemanSprite(enemyData.X, enemyData.Y, levelManager, player),
        "Tumbleweed" => EnemyFactory.Instance.CreateTumbleweedSprite(enemyData.X, enemyData.Y),
        "Cactus" => EnemyFactory.Instance.CreateCactusSprite(enemyData.X, enemyData.Y),
        "Boss" => EnemyFactory.Instance.CreateBossSprite(enemyData.X, enemyData.Y, levelManager),
        _ => null
      };

      if (newEnemy != null) {
        ((ABaseEnemy) newEnemy).Health = enemyData.Health;
        newEnemy.Direction = enemyData.Direction;
        restoredEnemies.Add(newEnemy);
      }
    }

    levelManager.CurrentLevel.RestoreEnemies(restoredEnemies);
  }
}
