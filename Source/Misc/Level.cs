using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GameProject.Factories;
using GameProject.Interfaces;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameProject.Misc;

internal partial class Level : ILevel {
  [GeneratedRegex(@"\r?\n")]
  private static partial Regex NewlineSplitRegex();

  private static readonly int BLOCK_WIDTH = 64;
  private static readonly int BLOCK_HEIGHT = 64;

  private Game1 game;
  private List<IBlock> nonCollidableBlocks = new(); // for non-collidable collidableBlocks -Aaron
  private List<IBlock> collidableBlocks = new();
  private List<IEnemy> enemies = new();
  private List<IWorldPickup> pickups = new();
  public Vector2 PlayerPosition { get; private set; }

  private Level(Game1 game) {
    this.game = game;
  }

  public static Level FromString(Game1 game, string levelDataString) {
    var level = new Level(game);

    var lines = NewlineSplitRegex().Split(levelDataString.Trim());

    var levelData = lines.Select((line) => line.Split(',')).ToArray();

    bool playerPositionSet = false;

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
                      level.collidableBlocks.Add(game.BlockFactory.CreateLogBlockSprite(xPos, yPos));
                      break;

                    case "1":
                      /* corner */
                      level.collidableBlocks.Add(game.BlockFactory.CreateLogCornerBlockSprite(xPos, yPos));
                      break;

                    default:
                      throw new FormatException($"unrecognized level block/entity variation '{entrySplit[1]}'");
                  }
                  break;
                }

              case "2":
                /* door */
                // TODO - SS: add pairing info
                level.collidableBlocks.Add(game.BlockFactory.CreateSmallDoorBlockSprite(xPos, yPos));
                break;

              case "3":
                /* player position */
                if (playerPositionSet) {
                  throw new FormatException("default player position set twice in same level");
                } else {
                  level.PlayerPosition = new(xPos, yPos);
                  playerPositionSet = true;
                }
                break;

              case "4":
                /* snake */
                level.enemies.Add(EnemySpriteFactory.Instance.CreateSnakeSprite(xPos, yPos));
                break;

              case "5":
                /* sand */
                level.nonCollidableBlocks.Add(game.BlockFactory.CreateSandBlockSprite(xPos, yPos));
                break;

              case "6":
                /* red sand */
                level.nonCollidableBlocks.Add(game.BlockFactory.CreateRedSandBlockSprite(xPos, yPos));
                break;

              case "7":
                /* wood plank */
                level.nonCollidableBlocks.Add(game.BlockFactory.CreateWoodPlankBlockSprite(xPos, yPos));
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
                      level.collidableBlocks.Add(game.BlockFactory.CreateRockBlockSprite(xPos, yPos));
                      break;

                    case "1":
                      /* corner */
                      level.collidableBlocks.Add(game.BlockFactory.CreateRockCornerBlockSprite(xPos, yPos));
                      break;

                    case "2":
                      /* red X */
                      level.collidableBlocks.Add(game.BlockFactory.CreateRedXRockBlockSprite(xPos, yPos));
                      break;

                    case "3":
                      /* hole */
                      level.collidableBlocks.Add(game.BlockFactory.CreateRockHoleBlockSprite(xPos, yPos));
                      break;

                    default:
                      throw new FormatException($"unrecognized level block/entity variation '{variation}'");
                  }
                  break;
                }

              case "9":
                /* shotgunner */
                level.enemies.Add(EnemySpriteFactory.Instance.CreateShotgunnerSprite(xPos, yPos, game.ProjectileManager));
                break;

              case "10":
                /* bat */
                level.enemies.Add(EnemySpriteFactory.Instance.CreateBatSprite(xPos, yPos));
                break;

              case "11":
                /* rifleman */
                level.enemies.Add(EnemySpriteFactory.Instance.CreateRiflemanSprite(xPos, yPos, game.ProjectileManager));
                break;

              case "12":
                /* tumbleweed */
                level.enemies.Add(EnemySpriteFactory.Instance.CreateTumbleweedSprite(xPos, yPos));
                break;

              case "13":
                /* cactus */
                level.enemies.Add(EnemySpriteFactory.Instance.CreateCactusSprite(xPos, yPos));
                break;

              case "14":
                /* revolver */
                level.pickups.Add(new ItemWorldPickup(game.ItemSpriteFactory.CreateRevolver(xPos, yPos)));
                break;

              case "15":
                /* rifle */
                level.pickups.Add(new ItemWorldPickup(game.ItemSpriteFactory.CreateRifle(xPos, yPos)));
                break;

              case "16":
                /* shotgun */
                level.pickups.Add(new ItemWorldPickup(game.ItemSpriteFactory.CreateShotgun(xPos, yPos)));
                break;

              case "17":
                /* barrel */
                level.collidableBlocks.Add(game.BlockFactory.CreateBarrelBlockSprite(xPos, yPos));
                break;

              case "18":
                /* bar shelf */
                level.collidableBlocks.Add(game.BlockFactory.CreateBarShelfBlockSprite(xPos, yPos));
                break;

              case "19":
                /* shelf */
                level.collidableBlocks.Add(game.BlockFactory.CreateShelfBlockSprite(xPos, yPos));
                break;

              case "20":
                /* locked vault door */
                // TODO - SS: add pairing info
                level.collidableBlocks.Add(game.BlockFactory.CreateLockedVaultBlockSprite(xPos, yPos));
                break;

              case "21":
                /* open vault door */
                // TODO - SS: add pairing info
                level.collidableBlocks.Add(game.BlockFactory.CreateOpenVaultDoorBlockSprite(xPos, yPos));
                break;

              case "22":
                /* fire pit */
                level.collidableBlocks.Add(game.BlockFactory.CreateFirePitBlockSprite(xPos, yPos));
                break;

              case "23":
                /* fire */
                level.collidableBlocks.Add(game.BlockFactory.CreateFireBlockSprite(xPos, yPos));
                break;

              case "24":
                /* ladder */
                level.collidableBlocks.Add(game.BlockFactory.CreateLadderBlockSprite(xPos, yPos));
                break;

              case "25":
                /* mud */
                level.collidableBlocks.Add(game.BlockFactory.CreateMudBlockSprite(xPos, yPos));
                break;

              case "26":
                /* crate */
                level.collidableBlocks.Add(game.BlockFactory.CreateCrateBlockSprite(xPos, yPos));
                break;

              case "27":
                /* stool */
                level.collidableBlocks.Add(game.BlockFactory.CreateStoolBlockSprite(xPos, yPos));
                break;

              case "28":
                /* table */
                level.collidableBlocks.Add(game.BlockFactory.CreateTableBlockSprite(xPos, yPos));
                break;

              case "29":
                /* statue */
                level.collidableBlocks.Add(game.BlockFactory.CreateStatueBlockSprite(xPos, yPos));
                break;

              case "30":
                /* window */
                level.collidableBlocks.Add(game.BlockFactory.CreateWindowBlockSprite(xPos, yPos));
                break;

              case "31":
                /* locked slatted door */
                // TODO - SS: add pairing info
                level.collidableBlocks.Add(game.BlockFactory.CreateLockedSlattedDoorSprite(xPos, yPos));
                break;

              case "32":
                /* open slatted door */
                // TODO - SS: add pairing info
                level.collidableBlocks.Add(game.BlockFactory.CreateOpenSlattedDoorSprite(xPos, yPos));
                break;

              default:
                throw new FormatException($"unrecognized level block/entity type '{type}'");
            }
          }
        }
      }
    }

    if (!playerPositionSet) {
      throw new FormatException("default player position was not set in level");
    }

    return level;
  }

  public void Initialize() { }

  public void LoadContent(ContentManager content) { }

  public void Update(GameTime gameTime) {
    foreach (var collidableBlock in collidableBlocks) {
      collidableBlock.Update(gameTime);
    }

    foreach (var enemy in enemies) {
      enemy.Update(gameTime);
    }

    foreach (var pickup in pickups) {
      pickup.Update(gameTime);
    }
  }

  public void Draw(GameTime gameTime) {
    foreach (var nonCollidableBlock in nonCollidableBlocks) {
      nonCollidableBlock.Draw(game.SpriteBatch);
    }

    foreach (var collidableBlock in collidableBlocks) {
      collidableBlock.Draw(game.SpriteBatch);
    }

    foreach (var enemy in enemies) {
      enemy.Draw(game.SpriteBatch);
    }

    foreach (var pickup in pickups) {
      pickup.Draw(game.SpriteBatch);
    }
  }

  public void AddPickup(IWorldPickup pickup) {
    pickups.Add(pickup);
  }
}
