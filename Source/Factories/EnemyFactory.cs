using System;
using GameProject.Enemies;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Factories;

internal class EnemyFactory {
  private Texture2D? snakeTexture;
  private Texture2D? batTexture;
  private Texture2D? shotgunnerTexture;
  private Texture2D? riflemanTexture;
  private Texture2D? tumbleweedTexture;
  private Texture2D? cactusTexture;

  public static EnemyFactory Instance { get; } = new();

  private EnemyFactory() { }

  public void LoadAllTextures(ContentManager content) {
    snakeTexture = content.Load<Texture2D>("Enemies/snakeSpritesheet");
    batTexture = content.Load<Texture2D>("Enemies/batSpritesheet");
    shotgunnerTexture = content.Load<Texture2D>("Enemies/shotgunnerSpritesheet");
    riflemanTexture = content.Load<Texture2D>("Enemies/riflemanSpritesheet");
    tumbleweedTexture = content.Load<Texture2D>("Enemies/tumbleweedSprite");
    cactusTexture = content.Load<Texture2D>("Enemies/cactusSprite");
  }

  public IEnemy CreateSnakeSprite(float xPos, float yPos) {
    if (snakeTexture == null) {
      throw new InvalidOperationException("LoadAllTextuers not called");
    }

    return new Snake(snakeTexture, new Vector2(xPos, yPos));
  }

  public IEnemy CreateCactusSprite(float xPos, float yPos) {
    if (cactusTexture == null) {
      throw new InvalidOperationException("LoadAllTextuers not called");
    }

    return new Cactus(cactusTexture, new Vector2(xPos, yPos));
  }

  public IEnemy CreateTumbleweedSprite(float xPos, float yPos) {
    if (tumbleweedTexture == null) {
      throw new InvalidOperationException("LoadAllTextuers not called");
    }

    return new Tumbleweed(tumbleweedTexture, new Vector2(xPos, yPos));
  }

  public IEnemy CreateRiflemanSprite(float xPos, float yPos, ILevelManager levelManager) {
    if (riflemanTexture == null) {
      throw new InvalidOperationException("LoadAllTextuers not called");
    }

    return new Rifleman(riflemanTexture, new Vector2(xPos, yPos), levelManager);
  }
  public IEnemy CreateBatSprite(float xPos, float yPos) {
    if (batTexture == null) {
      throw new InvalidOperationException("LoadAllTextuers not called");
    }

    return new Bat(batTexture, new Vector2(xPos, yPos));
  }
  public IEnemy CreateShotgunnerSprite(float xPos, float yPos, ILevelManager levelManager) {
    if (shotgunnerTexture == null) {
      throw new InvalidOperationException("LoadAllTextuers not called");
    }

    return new Shotgunner(shotgunnerTexture, new Vector2(xPos, yPos), levelManager);
  }
}
