using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
  private List<IBlock> blocks = new();
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
          var cell = row[colIndex];
          var cellSplit = cell.Split(':');
          var type = cellSplit[0];

          float xPos = BLOCK_WIDTH * colIndex;
          float yPos = BLOCK_HEIGHT * rowIndex;

          switch (type) {
            case "0":
              /* empty block, do nothing */
              break;

            case "1":
              /* sand */
              level.blocks.Add(game.BlockFactory.CreateSandBlockSprite(xPos, yPos));
              break;

            case "2":
              break;

            case "3":
              break;

            case "4":
              break;

            default:
              throw new FormatException($"unrecognized level block/entity type '{type}'");
          }
        }
      }
    }

    return level;
  }

  public void Initialize() { }

  public void LoadContent(ContentManager content) { }

  public void Update(GameTime gameTime) {
    foreach (var block in blocks) {
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
