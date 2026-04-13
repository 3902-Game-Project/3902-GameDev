using GameProject.Enemies;
using GameProject.GlobalInterfaces;
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

  public static EnemySpriteFactory Instance { get; } = new();

  private EnemySpriteFactory() { }

  public void LoadAllTextures(ContentManager content) {
    snakeTexture = content.Load<Texture2D>("Enemies/snakeSpritesheet");
    batTexture = content.Load<Texture2D>("Enemies/batSpritesheet");
    shotgunnerTexture = content.Load<Texture2D>("Enemies/shotgunnerSpritesheet");
    riflemanTexture = content.Load<Texture2D>("Enemies/riflemanSpritesheet");
    tumbleweedTexture = content.Load<Texture2D>("Enemies/tumbleweedSprite");
    cactusTexture = content.Load<Texture2D>("Enemies/cactusSprite");
  }

  public IEnemy CreateSnakeSprite(float xPos, float yPos) {
    return new SnakeSprite(snakeTexture, new Vector2(xPos, yPos));
  }

  public IEnemy CreateCactusSprite(float xPos, float yPos) {
    return new CactusSprite(cactusTexture, new Vector2(xPos, yPos));
  }

  public IEnemy CreateTumbleweedSprite(float xPos, float yPos) {
    return new TumbleSprite(tumbleweedTexture, new Vector2(xPos, yPos));
  }

  public IEnemy CreateRiflemanSprite(float xPos, float yPos, ILevelManager levelManager) {
    return new RiflemanSprite(riflemanTexture, new Vector2(xPos, yPos), levelManager);
  }
  public IEnemy CreateBatSprite(float xPos, float yPos) {
    return new BatSprite(batTexture, new Vector2(xPos, yPos));
  }
  public IEnemy CreateShotgunnerSprite(float xPos, float yPos, ILevelManager levelManager) {
    return new ShotgunnerSprite(shotgunnerTexture, new Vector2(xPos, yPos), levelManager);
  }
}
