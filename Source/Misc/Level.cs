using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GameProject.Collisions;
using GameProject.Factories;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameProject.Misc;

internal partial class Level : ILevel {
  [GeneratedRegex(@"\r?\n")]
  private static partial Regex NewlineSplitRegex();

  private static readonly int BLOCK_WIDTH = 64;
  private static readonly int BLOCK_HEIGHT = 64;

  private Game1 game;
  private List<IBlock> nonCollidableBlocks = new(); // for non-collidable blocks -Aaron
  private List<IBlock> collidableBlocks = new();
  private List<IEnemy> enemies = new();
  private List<IWorldPickup> pickups = new();

  private Level(Game1 game) {
    this.game = game;
  }

  public static Level FromString(Game1 game, string levelDataString) {
    var level = new Level(game);

    var lines = NewlineSplitRegex().Split(levelDataString.Trim());

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
              /* empty/floor, do nothing */
              level.terrain.Add(game.BlockFactory.CreateSandBlockSprite(xPos, yPos));
              break;

            case "1":
              /* walls */
              level.blocks.Add(game.BlockFactory.CreateRockBlockSprite(xPos, yPos));
              break;

            case "2":
              // doors
              level.blocks.Add(game.BlockFactory.CreateSmallDoorBlockSprite(xPos, yPos));
              break;

            case "3":
              // player pos
              break;

            case "4":
              // enemy
              level.enemies.Add(EnemySpriteFactory.Instance.CreateSnakeSprite(xPos, yPos));
              level.terrain.Add(game.BlockFactory.CreateSandBlockSprite(xPos, yPos));
              break;
            case "5":
              break;
            case "6":
              level.blocks.Add(game.BlockFactory.CreateRockCornerBlockSprite(xPos, yPos));
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

                    default:
                      throw new FormatException($"unrecognized level block/entity variation '{variation}'");
                  }
                  break;
                }

              default:
                throw new FormatException($"unrecognized level block/entity type '{type}'");
            }
          }
        }
      }
    }

    return level;
  }

  public void Initialize() { }

  public void LoadContent(ContentManager content) { }

  public void Update(GameTime gameTime) {
    foreach (var block in collidableBlocks) {
      block.Update(gameTime);
      }

    foreach (var enemy in enemies) {
      enemy.Update(gameTime);
    }

    foreach (var pickup in pickups) {
      pickup.Update(gameTime);
    }
  }

  public void Draw(GameTime gameTime) {
    foreach (var terr in terrain) {
      terr.Draw(game.SpriteBatch);
    }
    foreach (var block in blocks) {
      block.Draw(game.SpriteBatch);
    }

    foreach (var enemy in enemies) {
      enemy.Update(gameTime);
    }

    foreach (var pickup in pickups) {
      pickup.Draw(game.SpriteBatch);
    }
  }

  public void AddPickup(IWorldPickup pickup) {
    pickups.Add(pickup);
  }
}
