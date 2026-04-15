using System.Collections.Generic;
using System.Linq;
using GameProject.Blocks;
using GameProject.Collisions;
using GameProject.Enemies;
using GameProject.Globals;
using GameProject.Managers;
using GameProject.PlayerSpace;
using GameProject.Projectiles;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Level;

internal class Level(
  List<IBlock> nonCollidableBlocks,
  List<IBlock> collidableBlocks,
  List<IBlock> doors,
  List<IEnemy> enemies,
  List<IWorldPickup> pickups,
  Vector2 playerPosition
) : ILevel {
  private readonly List<IEnemy> deadEnemies = [];

  private void CheckLevelClear() {
    var killableEnemies = enemies.Where(e => e is not CactusSprite);

    if (!killableEnemies.Any()) {
      foreach (var door in doors) {
        if (door is SmallDoorBlock smallDoorBlock) {
          smallDoorBlock.ChangeState(LockableDoorBlockState.Open);
        }
        if (door is SlattedDoorBlock slattedDoorBlock) {
          slattedDoorBlock.ChangeState(LockableDoorBlockState.Open);
        }
      }
    }
  }

  public List<IWorldPickup> Pickups => pickups;
  public ProjectileManager ProjectileManager { get; private set; } = new ProjectileManager();
  public CollisionManager CollisionManager { get; private set; } = new CollisionManager();

  public void Initialize() { }

  public void LoadContent(ContentManager content) { }

  public void Update(GameTime gameTime) {
    foreach (var nonCollidableBlocks in nonCollidableBlocks) {
      nonCollidableBlocks.Update(gameTime);
    }

    foreach (var collidableBlock in collidableBlocks) {
      collidableBlock.Update(gameTime);
    }

    foreach (var doorBlock in doors) {
      doorBlock.Update(gameTime);
    }

    for (int i = enemies.Count - 1; i >= 0; i--) {
      var enemy = enemies[i];

      enemy.Update(gameTime);

      if (enemy.Health <= 0) {
        deadEnemies.Add(enemy);
        enemies.Remove(enemy);
        CollisionManager.Remove(enemy);
      }
    }

    foreach (var deadEnemy in deadEnemies) {
      deadEnemy.Update(gameTime);
    }

    foreach (var pickup in pickups) {
      pickup.Update(gameTime);
    }

    for (int i = 0; i < ProjectileManager.Projectiles.Count; i++) {
      IProjectile projectile = ProjectileManager.Projectiles[i];
      if (projectile is ICollidable collidableProj) {
        CollisionManager.Add(collidableProj);
      }

      if (projectile.IsExpired) {
        if (projectile is ICollidable expiredProj) {
          CollisionManager.Remove(expiredProj);
        }
        ProjectileManager.Remove(projectile);
        i--;
      }
    }

    ProjectileManager.Update(gameTime);
    CollisionManager.Update(gameTime);

    CheckLevelClear();
  }

  public void Draw(SpriteBatch spriteBatch) {
    foreach (var nonCollidableBlock in nonCollidableBlocks) {
      nonCollidableBlock.Draw(spriteBatch);
    }

    foreach (var pickup in pickups) {
      pickup.Draw(spriteBatch);
    }

    foreach (var collidableBlock in collidableBlocks) {
      collidableBlock.Draw(spriteBatch);
    }

    foreach (var doorBlock in doors) {
      doorBlock.Draw(spriteBatch);
    }

    foreach (var enemy in enemies) {
      enemy.Draw(spriteBatch);
    }

    foreach (var deadEnemy in deadEnemies) {
      deadEnemy.Draw(spriteBatch);
    }

    ProjectileManager.Draw(spriteBatch);

    foreach (var enemy in enemies) {
      if (enemy is BaseEnemy baseEnemy && baseEnemy.Health > 0) {
        float enemyHealthPercent = MathHelper.Clamp((float) baseEnemy.Health / baseEnemy.MaxHealth, 0f, 1f);
        float scaleWidth = TextureStore.Instance.HealthBar.Width * 0.15f;
        Vector2 enemyHealthPositions = new(
          baseEnemy.Position.X - (scaleWidth / 2f),
          baseEnemy.Position.Y - baseEnemy.Collider.Height);
        spriteBatch.Draw(texture: TextureStore.Instance.HealthBar,
          position: enemyHealthPositions,
          sourceRectangle: null,
          color: Color.DarkSlateGray,
          rotation: 0f,
          origin: Vector2.Zero,
          scale: 0.15f,
          effects: SpriteEffects.None,
          layerDepth: 0f
          );
        int enemyHealthVisible = (int) (TextureStore.Instance.HealthBar.Width * enemyHealthPercent);
        Rectangle enemyHpSource = new(0, 0, enemyHealthVisible, TextureStore.Instance.HealthBar.Height);

        spriteBatch.Draw(
          texture: TextureStore.Instance.HealthBar,
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

  public Vector2 GetDefaultPlayerPosition() {
    return playerPosition;
  }

  public void AddPickup(IWorldPickup pickup) {
    pickups.Add(pickup);
  }

  public void RemovePickup(IWorldPickup pickup) {
    pickups.Remove(pickup);
  }

  public void LevelSwitchUpdateColliders(Player player) {
    CollisionManager.Clear();

    CollisionManager.Add(player);

    foreach (var block in collidableBlocks) {
      CollisionManager.Add(block);
    }

    foreach (var doorBlock in doors) {
      CollisionManager.Add(doorBlock);
    }

    foreach (var enemy in enemies) {
      CollisionManager.Add(enemy);
    }
  }

  public IEnumerable<IBlock> GetOpenableDoors() {
    return doors.Where(door => door is VaultDoorBlock || door is SlattedDoorBlock);
  }
}
