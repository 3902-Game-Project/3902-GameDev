using GameProject.Interfaces;
using GameProject.Managers;
using GameProject.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Factories;

internal class EnemySpriteFactory {
  private Texture2D snakeTexture;
  private Texture2D batTexture;
  private Texture2D shotgunnerTexture;
  private Texture2D riflemanTexture;
  private Texture2D tumbleweedTexture;
  private Texture2D cactusTexture;
  private static EnemySpriteFactory instance = new();

  public static EnemySpriteFactory Instance {
    get { return instance; }
  }

  private EnemySpriteFactory() {
  }

  public void LoadAllTextures(ContentManager content) {
    snakeTexture = content.Load<Texture2D>("snakeSpritesheet");
    batTexture = content.Load<Texture2D>("batSpritesheet");
    shotgunnerTexture = content.Load<Texture2D>("shotgunnerSpritesheet");
    riflemanTexture = content.Load<Texture2D>("rifleSpritesheet");
    tumbleweedTexture = content.Load<Texture2D>("tumbleweedSprite");
    cactusTexture = content.Load<Texture2D>("cactusSprite");
  }

  public IEnemy CreateSnakeSprite(float xPos, float yPos) {
    return new SnakeSprite(snakeTexture, new Vector2(xPos, yPos));
  }
  public IEnemy CreateCactusSprite(float xPos, float yPos) {
    return new CactusSprite(cactusTexture, new Vector2(400, 200));
  }
  public IEnemy CreateTumbleweedSprite(float xPos, float yPos) {
    return new TumbleSprite(tumbleweedTexture, new Vector2(400, 200));
  }
  public IEnemy CreateRiflemanSprite(float xPos, float yPos, ProjectileManager projectileManager) {
    return new RiflemanSprite(riflemanTexture, new Vector2(400, 200), projectileManager);
  }
  public IEnemy CreateBatSprite(float xPos, float yPos) {
    return new BatSprite(batTexture, new Vector2(400, 200));
  }
  public IEnemy CreateShotgunnerSprite(float xPos, float yPos, ProjectileManager projectileManager) {
    return new ShotgunnerSprite(shotgunnerTexture, new Vector2(400, 200), projectileManager);
  }
}
