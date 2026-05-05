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
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;

namespace GameProject.Level;

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

  private static void AddCellEntry(
    Player player,
    ILevelManager levelManager,
    ISet<string> levelNames,

    string entry,
    float xPos,
    float yPos,

    List<IBlock> nonCollidableBlocks,
    List<IBlock> collidableBlocks,
    List<IBlock> doors,
    List<IEnemy> enemies,
    List<IWorldPickup> pickups,
    ref Vector2? playerPositionNullable
  ) {
    var entrySplit = entry.Trim().Split(':');
    var type = entrySplit[0];

    switch (type) {
      case "":
      case "0":
        /* empty, do nothing */
        break;

      case "1": {
          /* log */

          if (entrySplit.Length != 2) {
            throw new FormatException($"Expected 1 parameter for level block/entity type '{type}'");
          }

          var variation = entrySplit[1];

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
              throw new FormatException($"unrecognized level block/entity variation '{entrySplit[1]}'");
          }
          break;
        }

      case "2": {
          /* small door */
          var stateString = entrySplit[1];
          var state = stateString switch {
            "0" => LockableDoorBlockState.Locked,
            "1" => LockableDoorBlockState.Open,
            _ => throw new FormatException($"unrecognized door state '{stateString}"),
          };
          var pairedLevelName = entrySplit[2];

          if (!levelNames.Contains(pairedLevelName)) {
            throw new FormatException($"unrecognized pairing level name '{pairedLevelName}'");
          }

          doors.Add(BlockFactory.CreateSmallDoorBlockSprite(xPos, yPos, state, pairedLevelName, levelManager));
          break;
        }

      case "3":
        /* player position */
        if (playerPositionNullable is not null) {
          throw new FormatException("default player position set twice in same level");
        } else {
          playerPositionNullable = new Vector2(xPos, yPos) + PLAYER_POSITION_OFFSET;
        }
        break;

      case "4":
        /* snake */
        enemies.Add(EnemyFactory.Instance.CreateSnakeSprite(xPos + ENEMY_POSITION_OFFSET.X, yPos + ENEMY_POSITION_OFFSET.Y));
        break;

      case "5":
        /* sand */
        nonCollidableBlocks.Add(BlockFactory.CreateSandBlockSprite(xPos, yPos));
        break;

      case "6":
        /* red sand */
        nonCollidableBlocks.Add(BlockFactory.CreateRedSandBlockSprite(xPos, yPos));
        break;

      case "7":
        /* wood plank */
        nonCollidableBlocks.Add(BlockFactory.CreateWoodPlankBlockSprite(xPos, yPos));
        break;

      case "8": {
          /* rock */

          if (entrySplit.Length < 2) {
            throw new FormatException($"Expected 1 parameter for level block/entity type '{type}'");
          }

          var variation = entrySplit[1];

          switch (variation) {
            case "0":
              /* wall */
              collidableBlocks.Add(BlockFactory.CreateRockBlockSprite(xPos, yPos));
              break;

            case "1":
              /* corner */
              collidableBlocks.Add(BlockFactory.CreateRockCornerBlockSprite(xPos, yPos));
              break;

            case "3": {
                /* hole to other room */
                var pairedLevelName = entrySplit[2];

                if (!levelNames.Contains(pairedLevelName)) {
                  throw new FormatException($"unrecognized pairing level name '{pairedLevelName}'");
                }

                doors.Add(BlockFactory.CreateRockHoleBlockSprite(xPos, yPos, pairedLevelName, levelManager));
                break;
              }

            default:
              throw new FormatException($"unrecognized level block/entity variation '{variation}'");
          }
          break;
        }

      case "9":
        /* shotgunner */
        enemies.Add(EnemyFactory.Instance.CreateShotgunnerSprite(xPos + ENEMY_POSITION_OFFSET.X, yPos + ENEMY_POSITION_OFFSET.Y, levelManager, player));
        break;

      case "10":
        /* bat */
        enemies.Add(EnemyFactory.Instance.CreateBatSprite(xPos + ENEMY_POSITION_OFFSET.X, yPos + ENEMY_POSITION_OFFSET.Y));
        break;

      case "11":
        /* rifleman */
        enemies.Add(EnemyFactory.Instance.CreateRiflemanSprite(xPos + ENEMY_POSITION_OFFSET.X, yPos + ENEMY_POSITION_OFFSET.Y, levelManager, player));
        break;

      case "12":
        /* tumbleweed */
        enemies.Add(EnemyFactory.Instance.CreateTumbleweedSprite(xPos + ENEMY_POSITION_OFFSET.X, yPos + ENEMY_POSITION_OFFSET.Y));
        break;

      case "13":
        /* cactus */
        enemies.Add(EnemyFactory.Instance.CreateCactusSprite(xPos + ENEMY_POSITION_OFFSET.X, yPos + ENEMY_POSITION_OFFSET.Y));
        break;

      case "14":
        /* revolver */
        pickups.Add(new ItemWorldPickup(ItemFactory.Instance.CreateRevolver(xPos, yPos, player, levelManager)));
        break;

      case "15":
        /* rifle */
        pickups.Add(new ItemWorldPickup(ItemFactory.Instance.CreateRifle(xPos, yPos, player, levelManager)));
        break;

      case "16":
        /* shotgun */
        pickups.Add(new ItemWorldPickup(ItemFactory.Instance.CreateShotgun(xPos, yPos, player, levelManager)));
        break;

      case "17":
        /* barrel */
        collidableBlocks.Add(BlockFactory.CreateBarrelBlockSprite(xPos, yPos));
        break;

      case "18":
        /* bar shelf */
        collidableBlocks.Add(BlockFactory.CreateBarShelfBlockSprite(xPos, yPos));
        break;

      case "19":
        /* shelf */
        collidableBlocks.Add(BlockFactory.CreateShelfBlockSprite(xPos, yPos));
        break;

      case "20": {
          /* vault door */
          var stateString = entrySplit[1];
          var state = stateString switch {
            "0" => VaultDoorBlockState.Locked,
            "1" => VaultDoorBlockState.Opening,
            "2" => VaultDoorBlockState.Open,
            _ => throw new FormatException($"unrecognized door state '{stateString}"),
          };
          var pairedLevelName = entrySplit[2];

          if (!levelNames.Contains(pairedLevelName)) {
            throw new FormatException($"unrecognized pairing level name '{pairedLevelName}'");
          }

          doors.Add(BlockFactory.CreateVaultDoorBlockSprite(xPos, yPos, state, pairedLevelName, levelManager));
          break;
        }

      case "21":
        /* key item */
        pickups.Add(new ItemWorldPickup(ItemFactory.CreateKey(xPos, yPos, levelManager)));
        break;

      case "22":
        /* fire pit */
        collidableBlocks.Add(BlockFactory.CreateFirePitBlockSprite(xPos, yPos));
        break;

      case "23":
        /* fire */
        collidableBlocks.Add(BlockFactory.CreateFireBlockSprite(xPos, yPos, player));
        break;

      case "25":
        /* mud */
        collidableBlocks.Add(BlockFactory.CreateMudBlockSprite(xPos, yPos));
        break;

      case "26":
        /* crate */
        collidableBlocks.Add(BlockFactory.CreateCrateBlockSprite(xPos, yPos));
        break;

      case "27":
        /* stool */
        collidableBlocks.Add(BlockFactory.CreateStoolBlockSprite(xPos, yPos));
        break;

      case "28":
        /* table */
        collidableBlocks.Add(BlockFactory.CreateTableBlockSprite(xPos, yPos));
        break;

      case "29":
        /* statue */
        collidableBlocks.Add(BlockFactory.CreateStatueBlockSprite(xPos, yPos));
        break;

      case "30":
        /* window */
        collidableBlocks.Add(BlockFactory.CreateWindowBlockSprite(xPos, yPos));
        break;

      case "31": {
          /* slatted door */
          var stateString = entrySplit[1];
          var state = stateString switch {
            "0" => LockableDoorBlockState.Locked,
            "1" => LockableDoorBlockState.Open,
            _ => throw new FormatException($"unrecognized door state '{stateString}"),
          };
          var pairedLevelName = entrySplit[2];

          if (!levelNames.Contains(pairedLevelName)) {
            throw new FormatException($"unrecognized pairing level name '{pairedLevelName}'");
          }

          doors.Add(BlockFactory.CreateSlattedDoorSprite(xPos, yPos, state, pairedLevelName, levelManager));
          break;
        }

      case "32":
        /* treasure block */
        collidableBlocks.Add(BlockFactory.CreateTreasureBlockSprite(xPos, yPos));
        break;

      case "33":
        /* ammo item */

        var ammoTypeString = entrySplit[1];

        var ammoType = ammoTypeString switch {
          "0" => AmmoType.Light,
          "1" => AmmoType.Heavy,
          "2" => AmmoType.Shells,
          _ => throw new FormatException($"unrecognized ammo type '{ammoTypeString}"),
        };

        var countString = entrySplit[2];

        int count;

        if (!int.TryParse(countString, out count)) {
          throw new FormatException($"ammo count not int: '{countString}");
        }

        if (count < 0) {
          throw new FormatException($"ammo count out of bounds int: '{count}");
        }

        pickups.Add(WorldPickupFactory.Instance.CreateAmmo(new Vector2(xPos + 32f, yPos + 32f), ammoType, count));
        break;

      case "34":
        /* bank shelf */
        collidableBlocks.Add(BlockFactory.CreateBankShelfBlockSprite(xPos, yPos));
        break;

      case "35":
        /* tellers desk */
        collidableBlocks.Add(BlockFactory.CreateTellersDeskBlockSprite(xPos, yPos));
        break;

      case "36":
        /* health item */
        pickups.Add(new ItemWorldPickup(ItemFactory.Instance.CreateHealthPotion(xPos, yPos, player)));
        break;

      case "37":
        /* invincibility item */
        pickups.Add(new ItemWorldPickup(ItemFactory.Instance.CreateInvincibilityPotion(xPos, yPos, player)));
        break;

      case "38":
        /* infinite ammo item */
        pickups.Add(new ItemWorldPickup(ItemFactory.Instance.CreateInfiniteAmmo(xPos, yPos, player)));
        break;

      case "39":
        /* boss */
        enemies.Add(EnemyFactory.Instance.CreateBossSprite(xPos + ENEMY_POSITION_OFFSET.X, yPos + ENEMY_POSITION_OFFSET.Y, levelManager));
        break;

      default:
        throw new FormatException($"unrecognized level block/entity type '{type}'");
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
      AddCellEntry(
        player,
        levelManager,
        levelNames,

        entry,
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
