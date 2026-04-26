using GameProject.Enemies;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Factories;

internal class EnemyFactory {
  private Texture2D snakeTexture;
  private Texture2D batTexture;
  private Texture2D shotgunnerTexture;
  private Texture2D riflemanTexture;
  private Texture2D tumbleweedTexture;
  private Texture2D cactusTexture;
  private Texture2D bossTexture;

  public static EnemyFactory Instance { get; } = new();

  private EnemyFactory() { }

  public void LoadAllTextures(ContentManager content) {
    snakeTexture = content.Load<Texture2D>("Enemies/snakeSpritesheet");
    batTexture = content.Load<Texture2D>("Enemies/batSpritesheet");
    shotgunnerTexture = content.Load<Texture2D>("Enemies/shotgunnerSpritesheet");
    riflemanTexture = content.Load<Texture2D>("Enemies/riflemanSpritesheet");
    tumbleweedTexture = content.Load<Texture2D>("Enemies/tumbleweedSprite");
    cactusTexture = content.Load<Texture2D>("Enemies/cactusSprite");
    bossTexture = content.Load<Texture2D>("Enemies/bossSprite");
  }

  public IEnemy CreateSnakeSprite(float xPos, float yPos) {
    return new Snake(snakeTexture, new Vector2(xPos, yPos));
  }

  public IEnemy CreateCactusSprite(float xPos, float yPos) {
    return new Cactus(cactusTexture, new Vector2(xPos, yPos));
  }

  public IEnemy CreateTumbleweedSprite(float xPos, float yPos) {
    return new Tumbleweed(tumbleweedTexture, new Vector2(xPos, yPos));
  }

  public IEnemy CreateRiflemanSprite(float xPos, float yPos, ILevelManager levelManager) {
    return new Rifleman(riflemanTexture, new Vector2(xPos, yPos), levelManager);
  }
  public IEnemy CreateBatSprite(float xPos, float yPos) {
    return new Bat(batTexture, new Vector2(xPos, yPos));
  }
  public IEnemy CreateShotgunnerSprite(float xPos, float yPos, ILevelManager levelManager) {
    return new Shotgunner(shotgunnerTexture, new Vector2(xPos, yPos), levelManager);
  }

  public IEnemy CreateBossSprite(float x, float y, ILevelManager levelManager) {
    // Assuming 'bossTexture' is loaded in your Factory. 
    // If you don't have one yet, you can pass in the Shotgunner texture temporarily just to see it render!
    return new Boss(bossTexture, new Vector2(x, y), levelManager);
  }
}
