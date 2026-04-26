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

#nullable enable

internal class Level : ILevel {
  private readonly List<IBlock> nonCollidableBlocks;
  private readonly List<IBlock> collidableBlocks;
  private readonly List<IBlock> doors;
  private readonly List<IEnemy> aliveEnemies;
  private readonly List<IEnemy> deadEnemies = [];
  private readonly List<IWorldPickup> pickups;
  private readonly Vector2 playerPosition;
  private readonly CollisionManager collisionManager = new();
  private readonly Player player;
  public bool HasKillableEnemiesRemaining => aliveEnemies.Any(e => e is not Cactus);
  private readonly bool isBfgLevel;
  private bool bfgSpawned = false;
  private readonly ILevelManager levelManager;

  private void CategorizeDeadEnemies() {
    for (int i = aliveEnemies.Count - 1; i >= 0; i--) {
      var enemy = aliveEnemies[i];

      if (enemy.Health <= 0) {
        deadEnemies.Add(enemy);
        aliveEnemies.Remove(enemy);
        collisionManager.Remove(enemy);
      }
    }
  }

  private void CheckLevelClear() {
    var killableEnemies = aliveEnemies.Where(e => e is not Cactus);

    if (!killableEnemies.Any()) {
      foreach (var door in doors) {
        if (door is SmallDoorBlock smallDoorBlock) {
          smallDoorBlock.ChangeState(LockableDoorBlockState.Open);
        }
        if (door is SlattedDoorBlock slattedDoorBlock) {
          slattedDoorBlock.ChangeState(LockableDoorBlockState.Open);
        }
      }

      // BFG Spawn Logic
      if (!bfgSpawned) {
        bfgSpawned = true; // prevent multi-spawns

        if (isBfgLevel) {
          // Spawn 3 BFGs around the center of the room
          var factory = Factories.ItemFactory.Instance;
          AddPickup(new ItemWorldPickup(factory.CreateBFG(480f, 280f, player, levelManager)));
          AddPickup(new ItemWorldPickup(factory.CreateFakeBFG(380f, 280f, player, levelManager)));
          AddPickup(new ItemWorldPickup(factory.CreateFakeBFG(580f, 280f, player, levelManager)));
        }
      }
    }
  }

  public Level(
    List<IBlock> nonCollidableBlocks,
    List<IBlock> collidableBlocks,
    List<IBlock> doors,
    List<IEnemy> enemies,
    List<IWorldPickup> pickups,
    Vector2 playerPosition,
    Player player,
    ILevelManager levelManager,
    bool isBfgLevel
) {
    this.nonCollidableBlocks = nonCollidableBlocks;
    this.collidableBlocks = collidableBlocks;
    this.doors = doors;
    aliveEnemies = enemies;
    this.pickups = pickups;
    this.playerPosition = playerPosition;
    this.player = player;
    this.levelManager = levelManager;
    this.isBfgLevel = isBfgLevel;

    CategorizeDeadEnemies();

    collisionManager.Add(player);

    foreach (var block in collidableBlocks) {
      collisionManager.Add(block);
    }

    foreach (var doorBlock in doors) {
      collisionManager.Add(doorBlock);
    }

    foreach (var enemy in aliveEnemies) {
      collisionManager.Add(enemy);
    }
  }

  public ProjectileManager ProjectileManager { get; private set; } = new ProjectileManager();

  public void Initialize() { }

  public void LoadContent(ContentManager content) { }

  public void Update(double deltaTime) {
    foreach (var nonCollidableBlocks in nonCollidableBlocks) {
      nonCollidableBlocks.Update(deltaTime);
    }

    foreach (var collidableBlock in collidableBlocks) {
      collidableBlock.Update(deltaTime);
    }

    foreach (var doorBlock in doors) {
      doorBlock.Update(deltaTime);
    }

    if (!Flags.HaltEnemies) {
      foreach (var deadEnemy in deadEnemies) {
        deadEnemy.Update(deltaTime);
      }

      foreach (var aliveEnemy in aliveEnemies) {
        aliveEnemy.Update(deltaTime);
        aliveEnemy.Target = player.Position;
      }

      CategorizeDeadEnemies();
    }

    foreach (var pickup in pickups) {
      pickup.Update(deltaTime);
    }

    for (int i = 0; i < ProjectileManager.Projectiles.Count; i++) {
      IProjectile projectile = ProjectileManager.Projectiles[i];

      if (projectile is ICollidable collidableProj) {
        collisionManager.Add(collidableProj);
      }

      if (projectile.IsExpired) {
        if (projectile is ICollidable expiredProj) {
          collisionManager.Remove(expiredProj);
        }
        ProjectileManager.Remove(projectile);
        i--;
      }
    }

    ProjectileManager.Update(deltaTime);
    collisionManager.Update();

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

    foreach (var enemy in aliveEnemies) {
      enemy.Draw(spriteBatch);
    }

    foreach (var deadEnemy in deadEnemies) {
      deadEnemy.Draw(spriteBatch);
    }

    ProjectileManager.Draw(spriteBatch);

    foreach (var enemy in aliveEnemies) {
      if (enemy is ABaseEnemy baseEnemy && baseEnemy.Health > 0) {
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

  public IEnumerable<IBlock> GetOpenableDoors() {
    return doors;
  }

  public IEnumerable<IWorldPickup> GetRemoveAmmoInRange(Vector2 position, float range) {
    for (int i = pickups.Count - 1; i >= 0; i--) {
      var pickup = pickups[i];

      if (pickup.IsAutoCollect) {
        if (Vector2.Distance(position, pickup.Position) < range) {
          yield return pickup;
          RemovePickup(pickup);
        }
      }
    }
  }

  public IWorldPickup? GetClosestPickupInRange(Vector2 position, float range) {
    IWorldPickup? closestPickup = null;
    float? closestDistance = null;

    foreach (var pickup in pickups) {
      float distance = Vector2.Distance(position, pickup.Position);

      if (distance < range && (closestDistance == null || distance < closestDistance)) {
        closestDistance = distance;
        closestPickup = pickup;
      }
    }

    return closestPickup;
  }

  public void PlayerResolveCollisions(ICollidable movingEntity, CollisionAxis axis = CollisionAxis.Both, float cornerTolerance = 3.0f) {
    collisionManager.ResolveCollisionsFor(movingEntity, axis, cornerTolerance);
  }
}
