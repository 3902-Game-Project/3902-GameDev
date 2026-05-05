using GameProject.Globals;
using GameProject.Level;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

internal class EnemyFactory {
  private Texture2D snakeTexture;
  private Texture2D batTexture;
  private Texture2D shotgunnerTexture;
  private Texture2D riflemanTexture;
  private Texture2D tumbleweedTexture;
  private Texture2D cactusTexture;

  public static EnemyFactory Instance { get; } = new();

  private EnemyFactory() { }

  public void LoadAllTextures(ContentManager contentManager) {
    snakeTexture = contentManager.Load<Texture2D>("Enemies/Snake Spritesheet");
    batTexture = contentManager.Load<Texture2D>("Enemies/Bat Spritesheet");
    shotgunnerTexture = contentManager.Load<Texture2D>("Enemies/Shotgunner Spritesheet");
    riflemanTexture = contentManager.Load<Texture2D>("Enemies/Rifleman Spritesheet");
    tumbleweedTexture = contentManager.Load<Texture2D>("Enemies/Tumbleweed Spritesheet");
    cactusTexture = contentManager.Load<Texture2D>("Enemies/Cactus Spritesheet");
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

  public IEnemy CreateRiflemanSprite(float xPos, float yPos, ILevelManager levelManager, Player player) {
    return new Rifleman(riflemanTexture, new Vector2(xPos, yPos), levelManager, player);
  }

  public IEnemy CreateBatSprite(float xPos, float yPos) {
    return new Bat(batTexture, new Vector2(xPos, yPos));
  }

  public IEnemy CreateShotgunnerSprite(float xPos, float yPos, ILevelManager levelManager, Player player) {
    return new Shotgunner(shotgunnerTexture, new Vector2(xPos, yPos), levelManager, player);
  }

  public IEnemy CreateBossSprite(float x, float y, ILevelManager levelManager) {
    // Assuming 'bossTexture' is loaded in your Factory. 
    // If you don't have one yet, you can pass in the Shotgunner texture temporarily just to see it render!
    return new Boss(TextureStore.Instance.Boss, new Vector2(x, y), levelManager);
  }
}
