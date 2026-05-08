using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GameProject.Blocks;
using GameProject.Commands;
using GameProject.Enemies;
using GameProject.Factories;
using GameProject.Globals;
using GameProject.Items;
using GameProject.PlayerSpace;
using GameProject.Projectiles;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;

namespace GameProject.Level;

internal delegate IBlock BlockCreationFunc(float x, float y);
internal delegate IBlock PlayerBlockCreationFunc(float x, float y, Player player);
internal delegate IEnemy EnemyCreationFunc(float x, float y);
internal delegate IEnemy FiringEnemyCreationFunc(float x, float y, CurrentLevelGetter GetCurrentLevel);
internal delegate IEnemy FiringItemEnemyCreationFunc(float x, float y, CurrentLevelGetter GetCurrentLevel, Player player);
internal delegate IItem GunItemCreationFunc(float xPos, float yPos, Player player, ProjectileManagerGetter GetProjectileManager);

internal delegate void CellEntryParseFunc(
  Player player,
  ILevelManager levelManager,
  ISet<string> levelNames,

  string type, // needed for error information
  string[] arguments,
  float xPos,
  float yPos,

  List<IBlock> nonCollidableBlocks,
  List<IBlock> collidableBlocks,
  List<IBlock> doors,
  List<IEnemy> enemies,
  List<IWorldPickup> pickups,
  ref Vector2? playerPositionNullable
);

internal partial class LevelLoader {
  private static readonly Vector2 PLAYER_POSITION_OFFSET = new(Constants.BASE_BLOCK_WIDTH / 2.0f, Constants.BASE_BLOCK_HEIGHT / 2.0f);
  private static readonly Vector2 ENEMY_POSITION_OFFSET = new(Constants.BASE_BLOCK_WIDTH / 2.0f, Constants.BASE_BLOCK_HEIGHT);
  private static readonly string FLAGS_LINE_START = "Flags: ";

  [GeneratedRegex(@"\r?\n")]
  private static partial Regex NewlineSplitRegex { get; }

  public static readonly float PLAYER_LEFT_BOUNDARY_THRESHOLD = Constants.BASE_BLOCK_WIDTH * 2.0f;
  public static readonly float PLAYER_TOP_BOUNDARY_THRESHOLD = Constants.BASE_BLOCK_WIDTH * 2.0f;
  public static readonly float PLAYER_RIGHT_BOUNDARY_THRESHOLD = Constants.LEVEL_WIDTH - Constants.BASE_BLOCK_WIDTH * 2.0f;
  public static readonly float PLAYER_BOTTOM_BOUNDARY_THRESHOLD = Constants.LEVEL_HEIGHT - Constants.BASE_BLOCK_WIDTH * 2.0f;
  public static readonly float PLAYER_LEFT_POS_AFTER_TELEPORT = Constants.BASE_BLOCK_WIDTH * 1.5f;
  public static readonly float PLAYER_TOP_POS_AFTER_TELEPORT = Constants.BASE_BLOCK_WIDTH * 1.5f;
  public static readonly float PLAYER_RIGHT_POS_AFTER_TELEPORT = Constants.LEVEL_WIDTH - Constants.BASE_BLOCK_WIDTH * 1.5f;
  public static readonly float PLAYER_BOTTOM_POS_AFTER_TELEPORT = Constants.LEVEL_HEIGHT - Constants.BASE_BLOCK_WIDTH * 1.5f;

  private static readonly Dictionary<string, CellEntryParseFunc> CELL_ENTRY_FUNCS = new() {
    { "4", CreateEnemyCreator(EnemyFactory.Instance.CreateSnakeSprite) }, /* snake */
    { "5", CreateNonCollidableBlockCreator(BlockFactory.CreateSandBlockSprite) }, /* sand */
    { "6", CreateNonCollidableBlockCreator(BlockFactory.CreateRedSandBlockSprite) }, /* red sand */
    { "7", CreateNonCollidableBlockCreator(BlockFactory.CreateWoodPlankBlockSprite) }, /* wood plank */
    { "9", CreateFiringItemEnemyCreator(EnemyFactory.Instance.CreateShotgunnerSprite) }, /* shotgunner */
    { "10", CreateEnemyCreator(EnemyFactory.Instance.CreateBatSprite) }, /* bat */
    { "11", CreateFiringItemEnemyCreator(EnemyFactory.Instance.CreateRiflemanSprite) }, /* rifleman */
    { "12", CreateEnemyCreator(EnemyFactory.Instance.CreateTumbleweedSprite) }, /* tumbleweed */
    { "13", CreateEnemyCreator(EnemyFactory.Instance.CreateCactusSprite) }, /* cactus */
    { "14", CreateGunItemCreator(ItemFactory.Instance.CreateRevolver) }, /* revolver */
    { "15", CreateGunItemCreator(ItemFactory.Instance.CreateRifle) }, /* rifle */
    { "16", CreateGunItemCreator(ItemFactory.Instance.CreateShotgun) }, /* shotgun */
    { "17", CreateCollidableBlockCreator(BlockFactory.CreateBarrelBlockSprite) }, /* barrel */
    { "18", CreateCollidableBlockCreator(BlockFactory.CreateBarShelfBlockSprite) }, /* bar shelf */
    { "19", CreateCollidableBlockCreator(BlockFactory.CreateShelfBlockSprite) }, /* shelf */
    { "22", CreateCollidableBlockCreator(BlockFactory.CreateFirePitBlockSprite) }, /* fire pit */
    { "23", CreateCollidablePlayerBlockCreator(BlockFactory.CreateFireBlockSprite) }, /* fire */
    { "25", CreateCollidableBlockCreator(BlockFactory.CreateMudBlockSprite) }, /* mud */
    { "26", CreateCollidableBlockCreator(BlockFactory.CreateCrateBlockSprite) }, /* crate */
    { "27", CreateCollidableBlockCreator(BlockFactory.CreateStoolBlockSprite) }, /* stool */
    { "28", CreateCollidableBlockCreator(BlockFactory.CreateTableBlockSprite) }, /* table */
    { "29", CreateCollidableBlockCreator(BlockFactory.CreateStatueBlockSprite) }, /* statue */
    { "30", CreateCollidableBlockCreator(BlockFactory.CreateWindowBlockSprite) }, /* window */
    { "32", CreateCollidableBlockCreator(BlockFactory.CreateTreasureBlockSprite) }, /* treasure block */
    { "34", CreateCollidableBlockCreator(BlockFactory.CreateBankShelfBlockSprite) }, /* bank shelf */
    { "35", CreateCollidableBlockCreator(BlockFactory.CreateTellersDeskBlockSprite) }, /* tellers desk */
    { "39", CreateFiringEnemyCreator(EnemyFactory.Instance.CreateBossSprite) }, /* boss */
  };

  private static CellEntryParseFunc CreateNonCollidableBlockCreator(BlockCreationFunc BlockCreator) {
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

  private static CellEntryParseFunc CreateCollidableBlockCreator(BlockCreationFunc BlockCreator) {
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

  private static CellEntryParseFunc CreateCollidablePlayerBlockCreator(PlayerBlockCreationFunc PlayerBlockCreator) {
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

  private static CellEntryParseFunc CreateEnemyCreator(EnemyCreationFunc EnemyCreator) {
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

  private static CellEntryParseFunc CreateFiringEnemyCreator(FiringEnemyCreationFunc FiringEnemyCreator) {
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

  private static CellEntryParseFunc CreateFiringItemEnemyCreator(FiringItemEnemyCreationFunc FiringItemEnemyCreator) {
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

  private static CellEntryParseFunc CreateGunItemCreator(GunItemCreationFunc GunItemCreator) {
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

  private static void ParseSingleFlag(LevelFlags flags, string flag) {
    switch (flag) {
      case "":
      case "None":
        /* do nothing */
        break;

      case "Cave":
        flags.Cave = true;
        break;

      case "VictoryLevel":
        flags.VictoryLevel = true;
        break;

      default:
        throw new FormatException($"unrecognized level flag: {flag}");
    }
  }

  private static LevelFlags ParseFlags(string flagString) {
    if (!flagString.StartsWith(FLAGS_LINE_START)) {
      throw new FormatException("level data flags entry invalid");
    }

    var flagSplit = flagString[FLAGS_LINE_START.Length..].Split(',');

    LevelFlags flags = new();

    foreach (var flagUntrimmed in flagSplit) {
      var flag = flagUntrimmed.Trim();

      ParseSingleFlag(flags, flag);
    }

    return flags;
  }

  private static void CheckEntryLength(string[] arguments, int length, string type) {
    if (arguments.Length != length) {
      throw new FormatException($"Expected {length - 1} parameter for level block/entity type '{type}'");
    }
  }

  private static void CheckEntryLengthGreaterThanEqual(string[] arguments, int length, string type) {
    if (arguments.Length < length) {
      throw new FormatException($"Expected at least {length - 1} parameter for level block/entity type '{type}'");
    }
  }

  private static void AddCellEntry(
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
  ) {
    switch (type) {
      case "":
      case "0":
        /* empty, do nothing */

        CheckEntryLength(arguments, 0, type);
        break;

      case "1": {
          /* log */

          CheckEntryLength(arguments, 1, type);

          var variation = arguments[0];

          switch (variation) {
            case "0":
              /* wall */
              collidableBlocks.Add(BlockFactory.CreateLogBlockSprite(xPos, yPos));
              break;

            case "1":
              /* corner */
              collidableBlocks.Add(BlockFactory.CreateLogCornerBlockSprite(xPos, yPos));
              break;

            default:
              throw new FormatException($"unrecognized level block/entity variation '{arguments[0]}'");
          }
          break;
        }

      case "2": {
          /* small door */

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

          doors.Add(BlockFactory.CreateSmallDoorBlockSprite(xPos, yPos, state, pairedLevelName, levelManager.SwitchLevel));
          break;
        }

      case "3":
        /* player position */

        CheckEntryLength(arguments, 0, type);

        if (playerPositionNullable is not null) {
          throw new FormatException("default player position set twice in same level");
        } else {
          playerPositionNullable = new Vector2(xPos, yPos) + PLAYER_POSITION_OFFSET;
        }
        break;

      case "8": {
          /* rock */

          CheckEntryLengthGreaterThanEqual(arguments, 1, type);

          var variation = arguments[0];

          switch (variation) {
            case "0":
              /* wall */

              CheckEntryLength(arguments, 1, type);

              collidableBlocks.Add(BlockFactory.CreateRockBlockSprite(xPos, yPos));
              break;

            case "1":
              /* corner */

              CheckEntryLength(arguments, 1, type);

              collidableBlocks.Add(BlockFactory.CreateRockCornerBlockSprite(xPos, yPos));
              break;

            case "3": {
                /* hole to other room */

                CheckEntryLength(arguments, 2, type);

                var pairedLevelName = arguments[1];

                if (!levelNames.Contains(pairedLevelName)) {
                  throw new FormatException($"unrecognized pairing level name '{pairedLevelName}'");
                }

                doors.Add(BlockFactory.CreateRockHoleBlockSprite(xPos, yPos, pairedLevelName, levelManager.SwitchLevel));
                break;
              }

            default:
              throw new FormatException($"unrecognized level block/entity variation '{variation}'");
          }
          break;
        }

      case "20": {
          /* vault door */

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

          doors.Add(BlockFactory.CreateVaultDoorBlockSprite(xPos, yPos, state, pairedLevelName, levelManager.SwitchLevel));
          break;
        }

      case "21":
        /* key item */

        CheckEntryLength(arguments, 0, type);

        pickups.Add(new ItemWorldPickup(ItemFactory.CreateKey(xPos, yPos, () => levelManager.CurrentLevel)));
        break;

      case "31": {
          /* slatted door */

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

          doors.Add(BlockFactory.CreateSlattedDoorSprite(xPos, yPos, state, pairedLevelName, levelManager.SwitchLevel));
          break;
        }

      case "33":
        /* ammo item */

        CheckEntryLength(arguments, 2, type);

        var ammoTypeString = arguments[0];

        var ammoType = ammoTypeString switch {
          "0" => AmmoType.Light,
          "1" => AmmoType.Heavy,
          "2" => AmmoType.Shells,
          _ => throw new FormatException($"unrecognized ammo type '{ammoTypeString}"),
        };

        var countString = arguments[1];

        int count;

        if (!int.TryParse(countString, out count)) {
          throw new FormatException($"ammo count not int: '{countString}");
        }

        if (count < 0) {
          throw new FormatException($"ammo count out of bounds int: '{count}");
        }

        pickups.Add(WorldPickupFactory.Instance.CreateAmmo(new Vector2(xPos + 32f, yPos + 32f), ammoType, count));
        break;

      case "36":
        /* health item */

        CheckEntryLength(arguments, 0, type);

        pickups.Add(new ItemWorldPickup(ItemFactory.Instance.CreateHealthPotion(xPos, yPos, player)));
        break;

      case "37":
        /* invincibility item */

        CheckEntryLength(arguments, 0, type);

        pickups.Add(new ItemWorldPickup(ItemFactory.Instance.CreateInvincibilityPotion(xPos, yPos, player)));
        break;

      case "38":
        /* infinite ammo item */

        CheckEntryLength(arguments, 0, type);

        pickups.Add(new ItemWorldPickup(ItemFactory.Instance.CreateInfiniteAmmo(xPos, yPos, player)));
        break;

      default:
        if (CELL_ENTRY_FUNCS.TryGetValue(type, out var EntryParseFunc)) {
          EntryParseFunc(
            player,
            levelManager,
            levelNames,

            type,
            arguments,
            xPos,
            yPos,

            nonCollidableBlocks,
            collidableBlocks,
            doors,
            enemies,
            pickups,
            ref playerPositionNullable
          );
        } else {
          throw new FormatException($"unrecognized level block/entity type '{type}'");
        }
        break;
    }
  }

  public static void FromStringCell(
    Player player,
    ILevelManager levelManager,
    ISet<string> levelNames,

    string cell,
    int rowIndex,
    int colIndex,

    List<IBlock> nonCollidableBlocks,
    List<IBlock> collidableBlocks,
    List<IBlock> doors,
    List<IEnemy> enemies,
    List<IWorldPickup> pickups,
    ref Vector2? playerPositionNullable
  ) {
    float xPos = Constants.BASE_BLOCK_WIDTH * colIndex;
    float yPos = Constants.BASE_BLOCK_HEIGHT * rowIndex;

    var cellSplit = cell.Split(';');

    foreach (var entry in cellSplit) {
      var entrySplit = entry.Trim().Split(':');
      var type = entrySplit[0];
      var arguments = entrySplit[1..];

      AddCellEntry(
        player,
        levelManager,
        levelNames,

        type,
        arguments,
        xPos,
        yPos,

        nonCollidableBlocks,
        collidableBlocks,
        doors,
        enemies,
        pickups,
        ref playerPositionNullable
      );
    }
  }

  public static Level FromString(Player player, IGPCommand winScreenCommand, ILevelManager levelManager, ISet<string> levelNames, string levelDataString) {
    List<IBlock> nonCollidableBlocks = [];
    List<IBlock> collidableBlocks = [];
    List<IBlock> doors = [];
    List<IEnemy> enemies = [];
    List<IWorldPickup> pickups = [];
    Vector2? playerPositionNullable = null;

    var lines = NewlineSplitRegex.Split(levelDataString.Trim());

    if (lines.Length < 1) {
      throw new FormatException("level data does not contain a flags entry");
    }

    // parse flags

    var flags = ParseFlags(lines[0]);

    // parse level data

    var levelData = lines[2..].Select(line => line.Split(',')).ToArray();

    for (int rowIndex = 0; rowIndex < levelData.Length; rowIndex++) {
      var row = levelData[rowIndex];

      if (row.Length != levelData[0].Length) {
        throw new FormatException("line #" + (rowIndex + 1) + " length (" + row.Length + ") does not match first line length (" + levelData[0].Length + ")");
      }

      for (int colIndex = 0; colIndex < levelData[0].Length; colIndex++) {
        var cell = row[colIndex];

        FromStringCell(
          player,
          levelManager,
          levelNames,

          cell,
          rowIndex,
          colIndex,

          nonCollidableBlocks,
          collidableBlocks,
          doors,
          enemies,
          pickups,
          ref playerPositionNullable
        );
      }
    }

    if (playerPositionNullable is Vector2 playerPosition) {
      var level = new Level(
        levelFlags: flags,
        nonCollidableBlocks: nonCollidableBlocks,
        collidableBlocks: collidableBlocks,
        doors: doors,
        enemies: enemies,
        pickups: pickups,
        playerPosition: playerPosition,
        player: player,
        levelManager: levelManager,
        winScreenCommand: winScreenCommand
      );

      return level;
    } else {
      throw new FormatException("default player position was not set in level");
    }
  }
}
