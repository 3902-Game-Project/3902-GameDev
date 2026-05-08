using System;
using System.Collections.Generic;
using System.Linq;
using GameProject.Blocks;
using GameProject.Collisions;
using GameProject.Commands;
using GameProject.Enemies;
using GameProject.Globals;
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
  private readonly ILevelManager levelManager;
  private readonly IGPCommand winScreenCommand;
  private double victoryTimer = 0.0;

  private IEnumerable<IWorldPickup> GetRemoveAmmoInRange(Vector2 position, float range) {
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

  public IEnumerable<IEnemy> GetAliveEnemies() {
    return aliveEnemies;
  }

  public void RestoreEnemies(List<IEnemy> restoredEnemies) {
    foreach (var enemy in aliveEnemies) collisionManager.Remove(enemy);
    foreach (var enemy in deadEnemies) collisionManager.Remove(enemy);

    aliveEnemies.Clear();
    deadEnemies.Clear();

    aliveEnemies.AddRange(restoredEnemies);
    foreach (var enemy in aliveEnemies) collisionManager.Add(enemy);
  }

  private void CategorizeDeadEnemies() {
    for (int i = aliveEnemies.Count - 1; i >= 0; i--) {
      var enemy = aliveEnemies[i];

      if (enemy.IsDead()) {
        deadEnemies.Add(enemy);
        aliveEnemies.Remove(enemy);
        collisionManager.Remove(enemy);
      }
    }
  }

  private void CheckLevelClear() {
    if (!HasKillableEnemiesRemaining()) {
      foreach (var door in doors) {
        if (door is SmallDoorBlock smallDoorBlock) {
          smallDoorBlock.ChangeState(LockableDoorBlockState.Open);
        }
        if (door is SlattedDoorBlock slattedDoorBlock) {
          slattedDoorBlock.ChangeState(LockableDoorBlockState.Open);
        }
      }

      // BFG Spawn Logic
      levelManager.CheckBfgSpawnable();
    }
  }

  public Level(
    LevelFlags levelFlags,
    List<IBlock> nonCollidableBlocks,
    List<IBlock> collidableBlocks,
    List<IBlock> doors,
    List<IEnemy> enemies,
    List<IWorldPickup> pickups,
    Vector2 playerPosition,
    Player player,
    ILevelManager levelManager,
    IGPCommand winScreenCommand
) {
    LevelFlags = levelFlags;
    this.nonCollidableBlocks = nonCollidableBlocks;
    this.collidableBlocks = collidableBlocks;
    this.doors = doors;
    aliveEnemies = enemies;
    this.pickups = pickups;
    this.playerPosition = playerPosition;
    this.player = player;
    this.levelManager = levelManager;
    this.winScreenCommand = winScreenCommand;

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

  public LevelFlags LevelFlags { get; private set; }
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

    float xStep = player.Velocity.X * (float) deltaTime;
    float yStep = player.Velocity.Y * (float) deltaTime;
    player.Position += new Vector2(xStep, 0);
    PlayerResolveCollisions(player, CollisionAxis.X, MathF.Abs(yStep) + Constants.COLLISION_BUFFER);

    player.Position += new Vector2(0, yStep);
    PlayerResolveCollisions(player, CollisionAxis.Y, MathF.Abs(xStep) + Constants.COLLISION_BUFFER);

    // Auto-Collect for ammo
    foreach (var pickup in GetRemoveAmmoInRange(player.Position, Constants.AMMO_AUTO_COLLECT_RANGE)) {
      pickup.OnPickup(player);
    }

    // Process player interactions
    if (player.WantsToInteract) {
      IWorldPickup? closestPickup = GetClosestPickupInRange(player.Position, Constants.ITEM_GRAB_RANGE);
      if (closestPickup != null) {
        closestPickup.OnPickup(player);
        RemovePickup(closestPickup);
      }
      player.WantsToInteract = false; // Reset to false to prevent infinite reads
    }

    while (player.Inventory.DroppedItems.Count > 0) {
      AddPickup(player.Inventory.DroppedItems.Dequeue());
    }

    CheckLevelClear();

    if (LevelFlags.VictoryLevel) {
      victoryTimer += deltaTime;
      if (victoryTimer >= Constants.WIN_SCREEN_WAIT) {
        winScreenCommand.Execute();
      }
    }
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
  }

  public Vector2 GetDefaultPlayerPosition() {
    return playerPosition;
  }

  public bool HasKillableEnemiesRemaining() {
    return aliveEnemies.Where(enemy => !enemy.Invulnerable).Any();
  }

  public void KillAllDamageableEnemies() {
    foreach (var enemy in aliveEnemies) {
      if (!enemy.Invulnerable) {
        enemy.Kill();
      }
    }
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
