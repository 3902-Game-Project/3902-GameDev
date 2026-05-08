using System;
using System.Collections.Generic;
using GameProject.Blocks;
using GameProject.Enemies;
using GameProject.Globals;
using GameProject.Items;
using GameProject.PlayerSpace;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;

namespace GameProject.Level.Loader;

internal static class LevelLoaderCreatorFuncs {
  private static readonly Vector2 PLAYER_POSITION_OFFSET = new(Constants.BASE_BLOCK_WIDTH / 2.0f, Constants.BASE_BLOCK_HEIGHT / 2.0f);
  private static readonly Vector2 ENEMY_POSITION_OFFSET = new(Constants.BASE_BLOCK_WIDTH / 2.0f, Constants.BASE_BLOCK_HEIGHT);
  private static readonly Vector2 AMMO_POSITION_OFFSET = new(Constants.BASE_BLOCK_WIDTH / 2.0f, Constants.BASE_BLOCK_HEIGHT / 2.0f);

  private static void CheckEntryLength(string[] arguments, int length, string type) {
    if (arguments.Length != length) {
      throw new FormatException($"Expected {length - 1} parameter for level block/entity type '{type}'");
    }
  }

  public static CellEntryParseFunc CreateEmptyCreator() {
    return (
      Player player,
      ILevelManager levelManager,
      ISet<string> levelNames,

      string type,
      string[] arguments,
      float xPos,
      float yPos,

      List<IBlock> nonCollidableBlocks,
      List<IBlock> collidableBlocks,
      List<IBlock> doors,
      List<IEnemy> enemies,
      List<IWorldPickup> pickups,
      ref Vector2? playerPositionNullable
    ) => {
      CheckEntryLength(arguments, 0, type);
    };
  }

  public static CellEntryParseFunc CreateNonCollidableBlockCreator(BlockCreationFunc BlockCreator) {
    return (
      Player player,
      ILevelManager levelManager,
      ISet<string> levelNames,

      string type,
      string[] arguments,
      float xPos,
      float yPos,

      List<IBlock> nonCollidableBlocks,
      List<IBlock> collidableBlocks,
      List<IBlock> doors,
      List<IEnemy> enemies,
      List<IWorldPickup> pickups,
      ref Vector2? playerPositionNullable
    ) => {
      CheckEntryLength(arguments, 0, type);

      nonCollidableBlocks.Add(BlockCreator(xPos, yPos));
    };
  }

  public static CellEntryParseFunc CreateCollidableBlockCreator(BlockCreationFunc BlockCreator) {
    return (
      Player player,
      ILevelManager levelManager,
      ISet<string> levelNames,

      string type,
      string[] arguments,
      float xPos,
      float yPos,

      List<IBlock> nonCollidableBlocks,
      List<IBlock> collidableBlocks,
      List<IBlock> doors,
      List<IEnemy> enemies,
      List<IWorldPickup> pickups,
      ref Vector2? playerPositionNullable
    ) => {
      CheckEntryLength(arguments, 0, type);

      collidableBlocks.Add(BlockCreator(xPos, yPos));
    };
  }

  public static CellEntryParseFunc CreateCollidablePlayerBlockCreator(PlayerBlockCreationFunc PlayerBlockCreator) {
    return (
      Player player,
      ILevelManager levelManager,
      ISet<string> levelNames,

      string type,
      string[] arguments,
      float xPos,
      float yPos,

      List<IBlock> nonCollidableBlocks,
      List<IBlock> collidableBlocks,
      List<IBlock> doors,
      List<IEnemy> enemies,
      List<IWorldPickup> pickups,
      ref Vector2? playerPositionNullable
    ) => {
      CheckEntryLength(arguments, 0, type);

      collidableBlocks.Add(PlayerBlockCreator(xPos, yPos, player));
    };
  }

  public static CellEntryParseFunc CreateCollidableHoleBlockCreator(HoleBlockCreationFunc HoleBlockCreator) {
    return (
      Player player,
      ILevelManager levelManager,
      ISet<string> levelNames,

      string type,
      string[] arguments,
      float xPos,
      float yPos,

      List<IBlock> nonCollidableBlocks,
      List<IBlock> collidableBlocks,
      List<IBlock> doors,
      List<IEnemy> enemies,
      List<IWorldPickup> pickups,
      ref Vector2? playerPositionNullable
    ) => {
      CheckEntryLength(arguments, 1, type);

      var pairedLevelName = arguments[0];

      if (!levelNames.Contains(pairedLevelName)) {
        throw new FormatException($"unrecognized pairing level name '{pairedLevelName}'");
      }

      collidableBlocks.Add(HoleBlockCreator(xPos, yPos, pairedLevelName, levelManager.SwitchLevel));
    };
  }

  public static CellEntryParseFunc CreateCollidableLockableDoorBlockCreator(LockableDoorBlockCreationFunc LockableDoorBlockCreator) {
    return (
      Player player,
      ILevelManager levelManager,
      ISet<string> levelNames,

      string type,
      string[] arguments,
      float xPos,
      float yPos,

      List<IBlock> nonCollidableBlocks,
      List<IBlock> collidableBlocks,
      List<IBlock> doors,
      List<IEnemy> enemies,
      List<IWorldPickup> pickups,
      ref Vector2? playerPositionNullable
    ) => {
      CheckEntryLength(arguments, 2, type);

      var stateString = arguments[0];

      var state = stateString switch {
        "0" => LockableDoorBlockState.Locked,
        "1" => LockableDoorBlockState.Open,
        _ => throw new FormatException($"unrecognized door state '{stateString}"),
      };

      var pairedLevelName = arguments[1];

      if (!levelNames.Contains(pairedLevelName)) {
        throw new FormatException($"unrecognized pairing level name '{pairedLevelName}'");
      }

      collidableBlocks.Add(LockableDoorBlockCreator(xPos, yPos, state, pairedLevelName, levelManager.SwitchLevel));
    };
  }

  public static CellEntryParseFunc CreateCollidableVaultDoorBlockCreator(VaultDoorBlockCreationFunc VaultDoorBlockCreator) {
    return (
      Player player,
      ILevelManager levelManager,
      ISet<string> levelNames,

      string type,
      string[] arguments,
      float xPos,
      float yPos,

      List<IBlock> nonCollidableBlocks,
      List<IBlock> collidableBlocks,
      List<IBlock> doors,
      List<IEnemy> enemies,
      List<IWorldPickup> pickups,
      ref Vector2? playerPositionNullable
    ) => {
      CheckEntryLength(arguments, 2, type);

      var stateString = arguments[0];

      var state = stateString switch {
        "0" => VaultDoorBlockState.Locked,
        "1" => VaultDoorBlockState.Opening,
        "2" => VaultDoorBlockState.Open,
        _ => throw new FormatException($"unrecognized door state '{stateString}"),
      };

      var pairedLevelName = arguments[1];

      if (!levelNames.Contains(pairedLevelName)) {
        throw new FormatException($"unrecognized pairing level name '{pairedLevelName}'");
      }

      collidableBlocks.Add(VaultDoorBlockCreator(xPos, yPos, state, pairedLevelName, levelManager.SwitchLevel));
    };
  }

  public static CellEntryParseFunc CreateEnemyCreator(EnemyCreationFunc EnemyCreator) {
    return (
      Player player,
      ILevelManager levelManager,
      ISet<string> levelNames,

      string type,
      string[] arguments,
      float xPos,
      float yPos,

      List<IBlock> nonCollidableBlocks,
      List<IBlock> collidableBlocks,
      List<IBlock> doors,
      List<IEnemy> enemies,
      List<IWorldPickup> pickups,
      ref Vector2? playerPositionNullable
    ) => {
      CheckEntryLength(arguments, 0, type);

      enemies.Add(EnemyCreator(xPos + ENEMY_POSITION_OFFSET.X, yPos + ENEMY_POSITION_OFFSET.Y));
    };
  }

  public static CellEntryParseFunc CreateFiringEnemyCreator(FiringEnemyCreationFunc FiringEnemyCreator) {
    return (
      Player player,
      ILevelManager levelManager,
      ISet<string> levelNames,

      string type,
      string[] arguments,
      float xPos,
      float yPos,

      List<IBlock> nonCollidableBlocks,
      List<IBlock> collidableBlocks,
      List<IBlock> doors,
      List<IEnemy> enemies,
      List<IWorldPickup> pickups,
      ref Vector2? playerPositionNullable
    ) => {
      CheckEntryLength(arguments, 0, type);

      enemies.Add(FiringEnemyCreator(xPos + ENEMY_POSITION_OFFSET.X, yPos + ENEMY_POSITION_OFFSET.Y, () => levelManager.CurrentLevel));
    };
  }

  public static CellEntryParseFunc CreateFiringItemEnemyCreator(FiringItemEnemyCreationFunc FiringItemEnemyCreator) {
    return (
      Player player,
      ILevelManager levelManager,
      ISet<string> levelNames,

      string type,
      string[] arguments,
      float xPos,
      float yPos,

      List<IBlock> nonCollidableBlocks,
      List<IBlock> collidableBlocks,
      List<IBlock> doors,
      List<IEnemy> enemies,
      List<IWorldPickup> pickups,
      ref Vector2? playerPositionNullable
    ) => {
      CheckEntryLength(arguments, 0, type);

      enemies.Add(FiringItemEnemyCreator(xPos + ENEMY_POSITION_OFFSET.X, yPos + ENEMY_POSITION_OFFSET.Y, () => levelManager.CurrentLevel, player));
    };
  }

  public static CellEntryParseFunc CreateGunItemCreator(GunItemCreationFunc GunItemCreator) {
    return (
      Player player,
      ILevelManager levelManager,
      ISet<string> levelNames,

      string type,
      string[] arguments,
      float xPos,
      float yPos,

      List<IBlock> nonCollidableBlocks,
      List<IBlock> collidableBlocks,
      List<IBlock> doors,
      List<IEnemy> enemies,
      List<IWorldPickup> pickups,
      ref Vector2? playerPositionNullable
    ) => {
      CheckEntryLength(arguments, 0, type);

      pickups.Add(new ItemWorldPickup(GunItemCreator(xPos, yPos, player, () => levelManager.CurrentLevel.ProjectileManager)));
    };
  }

  public static CellEntryParseFunc CreateKeyItemCreator(KeyItemCreationFunc KeyItemCreator) {
    return (
      Player player,
      ILevelManager levelManager,
      ISet<string> levelNames,

      string type,
      string[] arguments,
      float xPos,
      float yPos,

      List<IBlock> nonCollidableBlocks,
      List<IBlock> collidableBlocks,
      List<IBlock> doors,
      List<IEnemy> enemies,
      List<IWorldPickup> pickups,
      ref Vector2? playerPositionNullable
    ) => {
      CheckEntryLength(arguments, 0, type);

      pickups.Add(new ItemWorldPickup(KeyItemCreator(xPos, yPos, () => levelManager.CurrentLevel)));
    };
  }

  public static CellEntryParseFunc CreatePlayerItemCreator(PlayerItemCreationFunc PlayerItemCreator) {
    return (
      Player player,
      ILevelManager levelManager,
      ISet<string> levelNames,

      string type,
      string[] arguments,
      float xPos,
      float yPos,

      List<IBlock> nonCollidableBlocks,
      List<IBlock> collidableBlocks,
      List<IBlock> doors,
      List<IEnemy> enemies,
      List<IWorldPickup> pickups,
      ref Vector2? playerPositionNullable
    ) => {
      CheckEntryLength(arguments, 0, type);

      pickups.Add(new ItemWorldPickup(PlayerItemCreator(xPos, yPos, player)));
    };
  }

  public static CellEntryParseFunc CreateAmmoItemCreator(AmmoItemCreationFunc AmmoItemCreator) {
    return (
      Player player,
      ILevelManager levelManager,
      ISet<string> levelNames,

      string type,
      string[] arguments,
      float xPos,
      float yPos,

      List<IBlock> nonCollidableBlocks,
      List<IBlock> collidableBlocks,
      List<IBlock> doors,
      List<IEnemy> enemies,
      List<IWorldPickup> pickups,
      ref Vector2? playerPositionNullable
    ) => {
      CheckEntryLength(arguments, 2, type);

      var ammoTypeString = arguments[0];

      var ammoType = ammoTypeString switch {
        "0" => AmmoType.Light,
        "1" => AmmoType.Heavy,
        "2" => AmmoType.Shells,
        _ => throw new FormatException($"unrecognized ammo type '{ammoTypeString}"),
      };

      var countString = arguments[1];

      if (!int.TryParse(countString, out var count)) {
        throw new FormatException($"ammo count not int: '{countString}");
      }

      if (count < 0) {
        throw new FormatException($"ammo count out of bounds int: '{count}");
      }

      pickups.Add(AmmoItemCreator(new Vector2(xPos, yPos) + AMMO_POSITION_OFFSET, ammoType, count));
    };
  }

  public static CellEntryParseFunc CreatePlayerPositionSetter() {
    return (
      Player player,
      ILevelManager levelManager,
      ISet<string> levelNames,

      string type,
      string[] arguments,
      float xPos,
      float yPos,

      List<IBlock> nonCollidableBlocks,
      List<IBlock> collidableBlocks,
      List<IBlock> doors,
      List<IEnemy> enemies,
      List<IWorldPickup> pickups,
      ref Vector2? playerPositionNullable
    ) => {
      CheckEntryLength(arguments, 0, type);

      if (playerPositionNullable is not null) {
        throw new FormatException("default player position set twice in same level");
      } else {
        playerPositionNullable = new Vector2(xPos, yPos) + PLAYER_POSITION_OFFSET;
      }
    };
  }
}
