using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameProject.Misc;

internal class Level : ILevel {
  private Game1 game;
  private List<IBlock> blocks = new();
  private List<IEnemy> enemies = new();

  private Level(Game1 game) {
    this.game = game;
  }

  public static Level FromString(Game1 game, string levelDataString) {
    var result = new Level(game);

    var lines = Regex.Split(levelDataString.Trim(), @"\r?\n"); 

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
          switch (type) {
            case "0":
              break;
            case "1":
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

    return result;
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
  }

  public void Draw(GameTime gameTime) {
    foreach (var block in blocks) {
      block.Draw(game.SpriteBatch);
    }

    foreach (var enemy in enemies) {
      enemy.Update(gameTime);
    }
  }
}
