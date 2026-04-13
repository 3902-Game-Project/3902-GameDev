using System.Collections.Generic;
using GameProject.Enemies;
using GameProject.Globals;
using GameProject.Interfaces;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Misc;

public class Level(
  List<IBlock> nonCollidableBlocks, // for non-collidable collidableBlocks -Aaron
  List<IBlock> collidableBlocks,
  List<IEnemy> enemies,
  List<IWorldPickup> pickups,
  Vector2 playerPosition
  ) : ILevel {
  public List<IBlock> CollidableBlocks => collidableBlocks;
  public List<IEnemy> Enemies => enemies;

  public List<IWorldPickup> Pickups => pickups;
  public Vector2 PlayerPosition { get; private set; } = playerPosition;
  public ProjectileManager ProjectileManager { get; private set; } = new ProjectileManager();

  public void Initialize() { }

  public void LoadContent(ContentManager content) { }

  public void Update(GameTime gameTime) {
    foreach (var nonCollidableBlocks in nonCollidableBlocks) {
      nonCollidableBlocks.Update(gameTime);
    }

    foreach (var collidableBlock in collidableBlocks) {
      collidableBlock.Update(gameTime);
    }

    foreach (var enemy in enemies) {
      enemy.Update(gameTime);
    }

    foreach (var pickup in pickups) {
      pickup.Update(gameTime);
    }

    ProjectileManager.Update(gameTime);
  }

  public void Draw(SpriteBatch spriteBatch) {
    foreach (var nonCollidableBlock in nonCollidableBlocks) {
      nonCollidableBlock.Draw(spriteBatch);
    }

    foreach (var collidableBlock in collidableBlocks) {
      collidableBlock.Draw(spriteBatch);
    }

    foreach (var enemy in enemies) {
      enemy.Draw(spriteBatch);
    }

    foreach (var pickup in pickups) {
      pickup.Draw(spriteBatch);
    }

    ProjectileManager.Draw(spriteBatch);

    foreach (var enemy in enemies) {
      if (enemy is BaseEnemy baseEnemy && baseEnemy.Health > 0) {
        float enemyHealthPercent = MathHelper.Clamp((float) baseEnemy.Health / baseEnemy.MaxHealth, 0f, 1f);
        float scaleWidth = TextureStore.HealthBar.Width * 0.15f;
        Vector2 enemyHealthPositions = new(
          baseEnemy.Position.X - (scaleWidth / 2f),
          baseEnemy.Position.Y - baseEnemy.Collider.Height);
        spriteBatch.Draw(texture: TextureStore.HealthBar,
          position: enemyHealthPositions,
          sourceRectangle: null,
          color: Color.DarkSlateGray,
          rotation: 0f,
          origin: Vector2.Zero,
          scale: 0.15f,
          effects: SpriteEffects.None,
          layerDepth: 0f
          );
        int enemyHealthVisible = (int) (TextureStore.HealthBar.Width * enemyHealthPercent);
        Rectangle enemyHpSource = new(0, 0, enemyHealthVisible, TextureStore.HealthBar.Height);

        spriteBatch.Draw(
          texture: TextureStore.HealthBar,
          position: enemyHealthPositions,
          sourceRectangle: enemyHpSource,
          color: Color.White,
          rotation: 0f,
          origin: Vector2.Zero,
          scale: 0.15f,
          effects: SpriteEffects.None,
          layerDepth: 0f
        );
      }
    }
  }

  public void AddPickup(IWorldPickup pickup) {
    pickups.Add(pickup);
  }

  public void RemovePickup(IWorldPickup pickup) {
    pickups.Remove(pickup);
  }
}
