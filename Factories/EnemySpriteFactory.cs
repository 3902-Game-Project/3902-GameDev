using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Factories;

internal class EnemySpriteFactory {
  private Texture2D snakeTexture;
  private Texture2D batTexture;
  private Texture2D shotgunnerTexture;
  private static EnemySpriteFactory instance = new EnemySpriteFactory();

  public static EnemySpriteFactory Instance {
    get { return instance; }
  }

  private EnemySpriteFactory() {
  }

  public void LoadAllTextures(ContentManager content) {
    snakeTexture = content.Load<Texture2D>("snakeSpritesheet");
    batTexture = content.Load<Texture2D>("batSpritesheet");
    shotgunnerTexture = content.Load<Texture2D>("shotgunnerSpritesheet");
  }

  public IEnemy CreateSnakeSprite() {
    return new SnakeSprite(snakeTexture, new Vector2(400, 200));
  }
  public IEnemy CreateBatSprite() {
    return new BatSprite(batTexture, new Vector2(400, 200));
  }
  public IEnemy CreateShotgunnerSprite() {
    return new ShotgunnerSprite(shotgunnerTexture, new Vector2(400, 200));
  }
}
