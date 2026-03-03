using System.Collections.Generic;
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

  public static Level FromString(Game1 game, string levelData) {
    var result = new Level(game);
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
