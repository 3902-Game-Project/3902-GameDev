using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GameProject.Factories;
using GameProject.Interfaces;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;

namespace GameProject.Misc;

internal partial class LevelLoader {
  private static readonly int BLOCK_WIDTH = 64;
  private static readonly int BLOCK_HEIGHT = 64;
  private static readonly Vector2 PLAYER_POSITION_OFFSET = new(BLOCK_WIDTH / 2.0f, BLOCK_HEIGHT / 2.0f);

  [GeneratedRegex(@"\r?\n")]
  private static partial Regex NewlineSplitRegex { get; }

  private static void AddCellEntry(
    Game1 game,
    ISet<string> levelNames,

    string type,
    string[] entrySplit,
    float xPos,
    float yPos,

    List<IBlock> nonCollidableBlocks,
    List<IBlock> collidableBlocks,
    List<IEnemy> enemies,
    List<IWorldPickup> pickups,
    ref Vector2? playerPositionNullable
  ) {
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
              collidableBlocks.Add(BlockSpriteFactory.Instance.CreateLogBlockSprite(xPos, yPos));
              break;

            case "1":
              /* corner */
              collidableBlocks.Add(BlockSpriteFactory.Instance.CreateLogCornerBlockSprite(xPos, yPos));
              break;

            default:
              throw new FormatException($"unrecognized level block/entity variation '{entrySplit[1]}'");
          }
          break;
        }

      case "2": {
          /* small door */
          var pairedLevelName = entrySplit[1];

          if (!levelNames.Contains(pairedLevelName)) {
            throw new FormatException($"unrecognized pairing level name '{pairedLevelName}'");
          }

          collidableBlocks.Add(BlockSpriteFactory.Instance.CreateSmallDoorBlockSprite(xPos, yPos, pairedLevelName, game.StateGame.LevelManager));
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
        enemies.Add(EnemySpriteFactory.Instance.CreateSnakeSprite(xPos, yPos));
        break;

      case "5":
        /* sand */
        nonCollidableBlocks.Add(BlockSpriteFactory.Instance.CreateSandBlockSprite(xPos, yPos));
        break;

      case "6":
        /* red sand */
        nonCollidableBlocks.Add(BlockSpriteFactory.Instance.CreateRedSandBlockSprite(xPos, yPos));
        break;

      case "7":
        /* wood plank */
        nonCollidableBlocks.Add(BlockSpriteFactory.Instance.CreateWoodPlankBlockSprite(xPos, yPos));
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
              collidableBlocks.Add(BlockSpriteFactory.Instance.CreateRockBlockSprite(xPos, yPos));
              break;

            case "1":
              /* corner */
              collidableBlocks.Add(BlockSpriteFactory.Instance.CreateRockCornerBlockSprite(xPos, yPos));
              break;

            case "2":
              /* red X */
              collidableBlocks.Add(BlockSpriteFactory.Instance.CreateRedXRockBlockSprite(xPos, yPos));
              break;

            case "3":
              /* hole */
              var pairedLevelName = entrySplit[2];

              if (!levelNames.Contains(pairedLevelName)) {
                throw new FormatException($"unrecognized pairing level name '{pairedLevelName}'");
              }

              collidableBlocks.Add(BlockSpriteFactory.Instance.CreateRockHoleBlockSprite(xPos, yPos, pairedLevelName));
              break;

            default:
              throw new FormatException($"unrecognized level block/entity variation '{variation}'");
          }
          break;
        }

      case "9":
        /* shotgunner */
        enemies.Add(EnemySpriteFactory.Instance.CreateShotgunnerSprite(xPos, yPos, game));
        break;

      case "10":
        /* bat */
        enemies.Add(EnemySpriteFactory.Instance.CreateBatSprite(xPos, yPos));
        break;

      case "11":
        /* rifleman */
        enemies.Add(EnemySpriteFactory.Instance.CreateRiflemanSprite(xPos, yPos, game));
        break;

      case "12":
        /* tumbleweed */
        enemies.Add(EnemySpriteFactory.Instance.CreateTumbleweedSprite(xPos, yPos));
        break;

      case "13":
        /* cactus */
        enemies.Add(EnemySpriteFactory.Instance.CreateCactusSprite(xPos, yPos));
        break;

      case "14":
        /* revolver */
        pickups.Add(new ItemWorldPickup(game.ItemSpriteFactory.CreateRevolver(xPos, yPos)));
        break;

      case "15":
        /* rifle */
        pickups.Add(new ItemWorldPickup(game.ItemSpriteFactory.CreateRifle(xPos, yPos)));
        break;

      case "16":
        /* shotgun */
        pickups.Add(new ItemWorldPickup(game.ItemSpriteFactory.CreateShotgun(xPos, yPos)));
        break;

      case "17":
        /* barrel */
        collidableBlocks.Add(BlockSpriteFactory.Instance.CreateBarrelBlockSprite(xPos, yPos));
        break;

      case "18":
        /* bar shelf */
        collidableBlocks.Add(BlockSpriteFactory.Instance.CreateBarShelfBlockSprite(xPos, yPos));
        break;

      case "19":
        /* shelf */
        collidableBlocks.Add(BlockSpriteFactory.Instance.CreateShelfBlockSprite(xPos, yPos));
        break;

      case "20": {
          /* vault door */
          var pairedLevelName = entrySplit[1];

          if (!levelNames.Contains(pairedLevelName)) {
            throw new FormatException($"unrecognized pairing level name '{pairedLevelName}'");
          }

          collidableBlocks.Add(BlockSpriteFactory.Instance.CreateVaultDoorBlockSprite(xPos, yPos, pairedLevelName, game.StateGame.LevelManager));
          break;
        }

      // case 21 - deleted for door consolidation -Aaron

      case "22":
        /* fire pit */
        collidableBlocks.Add(BlockSpriteFactory.Instance.CreateFirePitBlockSprite(xPos, yPos));
        break;

      case "23":
        /* fire */
        collidableBlocks.Add(BlockSpriteFactory.Instance.CreateFireBlockSprite(xPos, yPos));
        break;

      case "24":
        /* ladder */
        collidableBlocks.Add(BlockSpriteFactory.Instance.CreateLadderBlockSprite(xPos, yPos));
        break;

      case "25":
        /* mud */
        collidableBlocks.Add(BlockSpriteFactory.Instance.CreateMudBlockSprite(xPos, yPos));
        break;

      case "26":
        /* crate */
        collidableBlocks.Add(BlockSpriteFactory.Instance.CreateCrateBlockSprite(xPos, yPos));
        break;

      case "27":
        /* stool */
        collidableBlocks.Add(BlockSpriteFactory.Instance.CreateStoolBlockSprite(xPos, yPos));
        break;

      case "28":
        /* table */
        collidableBlocks.Add(BlockSpriteFactory.Instance.CreateTableBlockSprite(xPos, yPos));
        break;

      case "29":
        /* statue */
        collidableBlocks.Add(BlockSpriteFactory.Instance.CreateStatueBlockSprite(xPos, yPos));
        break;

      case "30":
        /* window */
        collidableBlocks.Add(BlockSpriteFactory.Instance.CreateWindowBlockSprite(xPos, yPos));
        break;

      case "31": {
          /* slatted door */
          var pairedLevelName = entrySplit[1];

          if (!levelNames.Contains(pairedLevelName)) {
            throw new FormatException($"unrecognized pairing level name '{pairedLevelName}'");
          }

          collidableBlocks.Add(BlockSpriteFactory.Instance.CreateSlattedDoorSprite(xPos, yPos, pairedLevelName, game.StateGame.LevelManager));
          break;
        }

      // case 32

      // case 33

      case "34":
        /* bank shelf */
        collidableBlocks.Add(BlockSpriteFactory.Instance.CreateBankShelfBlockSprite(xPos, yPos));
        break;

      case "35":
        /* tellers desk */
        collidableBlocks.Add(BlockSpriteFactory.Instance.CreateTellersDeskBlockSprite(xPos, yPos));
        break;

      default:
        throw new FormatException($"unrecognized level block/entity type '{type}'");
    }
  }

  public static Level FromString(Game1 game, ISet<string> levelNames, string levelDataString) {
    List<IBlock> nonCollidableBlocks = new();
    List<IBlock> collidableBlocks = new();
    List<IEnemy> enemies = new();
    List<IWorldPickup> pickups = new();
    Vector2? playerPositionNullable = null;

    var lines = NewlineSplitRegex.Split(levelDataString.Trim());

    var levelData = lines.Select((line) => line.Split(',')).ToArray();

    if (levelData.Length > 0) {
      for (int rowIndex = 0; rowIndex < levelData.Length; rowIndex++) {
        var row = levelData[rowIndex];

        if (row.Length != levelData[0].Length) {
          throw new FormatException("line #" + (rowIndex + 1) + " length (" + row.Length + ") does not match first line length (" + levelData[0].Length + ")");
        }

        for (int colIndex = 0; colIndex < levelData[0].Length; colIndex++) {
          float xPos = BLOCK_WIDTH * colIndex;
          float yPos = BLOCK_HEIGHT * rowIndex;

          var cell = row[colIndex];
          var cellSplit = cell.Split(';');

          foreach (var entry in cellSplit) {
            var entrySplit = entry.Trim().Split(':');
            var type = entrySplit[0];

            AddCellEntry(
              game,
              levelNames,

              type,
              entrySplit,
              xPos,
              yPos,

              nonCollidableBlocks,
              collidableBlocks,
              enemies,
              pickups,
              ref playerPositionNullable
            );
          }
        }
      }
    }

    if (playerPositionNullable is Vector2 playerPosition) {
      var level = new Level(game, nonCollidableBlocks, collidableBlocks, enemies, pickups, playerPosition);

      return level;
    } else {
      throw new FormatException("default player position was not set in level");
    }
  }
}
