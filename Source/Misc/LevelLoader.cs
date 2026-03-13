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

  [GeneratedRegex(@"\r?\n")]
  private static partial Regex NewlineSplitRegex { get; }

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

            switch (type) {
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
                      collidableBlocks.Add(game.BlockFactory.CreateLogBlockSprite(xPos, yPos));
                      break;

                    case "1":
                      /* corner */
                      collidableBlocks.Add(game.BlockFactory.CreateLogCornerBlockSprite(xPos, yPos));
                      break;

                    default:
                      throw new FormatException($"unrecognized level block/entity variation '{entrySplit[1]}'");
                  }
                  break;
                }

              case "2": {
                  /* open small door */
                  var pairedLevelName = entrySplit[1];

                  if (!levelNames.Contains(pairedLevelName)) {
                    throw new FormatException($"unrecognized pairing level name '{pairedLevelName}'");
                  }

                  collidableBlocks.Add(game.BlockFactory.CreateOpenSmallDoorBlockSprite(xPos, yPos, pairedLevelName));
                  break;
                }

              case "3":
                /* player position */
                if (playerPositionNullable is not null) {
                  throw new FormatException("default player position set twice in same level");
                } else {
                  playerPositionNullable = new(xPos, yPos);
                }
                break;

              case "4":
                /* snake */
                enemies.Add(EnemySpriteFactory.Instance.CreateSnakeSprite(xPos, yPos));
                break;

              case "5":
                /* sand */
                nonCollidableBlocks.Add(game.BlockFactory.CreateSandBlockSprite(xPos, yPos));
                break;

              case "6":
                /* red sand */
                nonCollidableBlocks.Add(game.BlockFactory.CreateRedSandBlockSprite(xPos, yPos));
                break;

              case "7":
                /* wood plank */
                nonCollidableBlocks.Add(game.BlockFactory.CreateWoodPlankBlockSprite(xPos, yPos));
                break;

              case "8": {
                  /* rock */

                  if (entrySplit.Length != 2) {
                    throw new FormatException($"Expected 1 parameter for level block/entity type '{type}'");
                  }

                  var variation = entrySplit[1];

                  switch (variation) {
                    case "0":
                      /* wall */
                      collidableBlocks.Add(game.BlockFactory.CreateRockBlockSprite(xPos, yPos));
                      break;

                    case "1":
                      /* corner */
                      collidableBlocks.Add(game.BlockFactory.CreateRockCornerBlockSprite(xPos, yPos));
                      break;

                    case "2":
                      /* red X */
                      collidableBlocks.Add(game.BlockFactory.CreateRedXRockBlockSprite(xPos, yPos));
                      break;

                    case "3":
                      /* hole */
                      collidableBlocks.Add(game.BlockFactory.CreateRockHoleBlockSprite(xPos, yPos));
                      break;

                    default:
                      throw new FormatException($"unrecognized level block/entity variation '{variation}'");
                  }
                  break;
                }

              case "9":
                /* shotgunner */
                enemies.Add(EnemySpriteFactory.Instance.CreateShotgunnerSprite(xPos, yPos, game.ProjectileManager));
                break;

              case "10":
                /* bat */
                enemies.Add(EnemySpriteFactory.Instance.CreateBatSprite(xPos, yPos));
                break;

              case "11":
                /* rifleman */
                enemies.Add(EnemySpriteFactory.Instance.CreateRiflemanSprite(xPos, yPos, game.ProjectileManager));
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
                collidableBlocks.Add(game.BlockFactory.CreateBarrelBlockSprite(xPos, yPos));
                break;

              case "18":
                /* bar shelf */
                collidableBlocks.Add(game.BlockFactory.CreateBarShelfBlockSprite(xPos, yPos));
                break;

              case "19":
                /* shelf */
                collidableBlocks.Add(game.BlockFactory.CreateShelfBlockSprite(xPos, yPos));
                break;

              case "20": {
                  /* locked vault door */
                  var pairedLevelName = entrySplit[1];

                  if (!levelNames.Contains(pairedLevelName)) {
                    throw new FormatException($"unrecognized pairing level name '{pairedLevelName}'");
                  }

                  collidableBlocks.Add(game.BlockFactory.CreateLockedVaultBlockSprite(xPos, yPos, pairedLevelName));
                  break;
                }

              case "21": {
                  /* open vault door */
                  var pairedLevelName = entrySplit[1];

                  if (!levelNames.Contains(pairedLevelName)) {
                    throw new FormatException($"unrecognized pairing level name '{pairedLevelName}'");
                  }

                  collidableBlocks.Add(game.BlockFactory.CreateOpenVaultDoorBlockSprite(xPos, yPos, pairedLevelName));
                  break;
                }

              case "22":
                /* fire pit */
                collidableBlocks.Add(game.BlockFactory.CreateFirePitBlockSprite(xPos, yPos));
                break;

              case "23":
                /* fire */
                collidableBlocks.Add(game.BlockFactory.CreateFireBlockSprite(xPos, yPos));
                break;

              case "24":
                /* ladder */
                collidableBlocks.Add(game.BlockFactory.CreateLadderBlockSprite(xPos, yPos));
                break;

              case "25":
                /* mud */
                collidableBlocks.Add(game.BlockFactory.CreateMudBlockSprite(xPos, yPos));
                break;

              case "26":
                /* crate */
                collidableBlocks.Add(game.BlockFactory.CreateCrateBlockSprite(xPos, yPos));
                break;

              case "27":
                /* stool */
                collidableBlocks.Add(game.BlockFactory.CreateStoolBlockSprite(xPos, yPos));
                break;

              case "28":
                /* table */
                collidableBlocks.Add(game.BlockFactory.CreateTableBlockSprite(xPos, yPos));
                break;

              case "29":
                /* statue */
                collidableBlocks.Add(game.BlockFactory.CreateStatueBlockSprite(xPos, yPos));
                break;

              case "30":
                /* window */
                collidableBlocks.Add(game.BlockFactory.CreateWindowBlockSprite(xPos, yPos));
                break;

              case "31": {
                  /* locked slatted door */
                  var pairedLevelName = entrySplit[1];

                  if (!levelNames.Contains(pairedLevelName)) {
                    throw new FormatException($"unrecognized pairing level name '{pairedLevelName}'");
                  }

                  collidableBlocks.Add(game.BlockFactory.CreateLockedSlattedDoorSprite(xPos, yPos, pairedLevelName));
                  break;
                }

              case "32": {
                  /* open slatted door */
                  var pairedLevelName = entrySplit[1];

                  if (!levelNames.Contains(pairedLevelName)) {
                    throw new FormatException($"unrecognized pairing level name '{pairedLevelName}'");
                  }

                  collidableBlocks.Add(game.BlockFactory.CreateOpenSlattedDoorSprite(xPos, yPos, pairedLevelName));
                  break;
                }

              case "33": {
                  /* locked small door */
                  var pairedLevelName = entrySplit[1];

                  if (!levelNames.Contains(pairedLevelName)) {
                    throw new FormatException($"unrecognized pairing level name '{pairedLevelName}'");
                  }

                  collidableBlocks.Add(game.BlockFactory.CreateLockedSmallDoorBlockSprite(xPos, yPos, pairedLevelName));
                  break;
                }
              case "34":
                /* bank shelf */
                collidableBlocks.Add(game.BlockFactory.CreateBankShelfBlockSprite(xPos, yPos));
                break;

              case "35":
                /* tellers desk */
                collidableBlocks.Add(game.BlockFactory.CreateTellersDeskBlockSprite(xPos, yPos));
                break;

              case "": {
                  break;
               }

              default:
                throw new FormatException($"unrecognized level block/entity type '{type}'");
            }
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
