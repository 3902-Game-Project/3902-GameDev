using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Factories;

internal class EnemySpriteFactory {
  private Texture2D snakeTexture;
  private Texture2D batTexture;
  private static EnemySpriteFactory instance = new EnemySpriteFactory();

  public static EnemySpriteFactory Instance {
    get { return instance; }
  }

  private EnemySpriteFactory() {
  }

  public void LoadAllTextures(ContentManager content) {
    snakeTexture = content.Load<Texture2D>("snakeSpritesheet");
  }

  public IEnemy CreateSnakeSprite() {
    return new SnakeSprite(snakeTexture, new Vector2(400, 200));
  }
}
