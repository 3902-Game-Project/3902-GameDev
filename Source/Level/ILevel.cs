using System.Collections.Generic;
using GameProject.Blocks;
using GameProject.Collisions;
using GameProject.Enemies;
using GameProject.GlobalInterfaces;
using GameProject.Projectiles;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;

namespace GameProject.Level;

#nullable enable

internal interface ILevel : IInitable, ITemporalUpdatable, IGPDrawable {
  internal LevelFlags LevelFlags { get; }
  internal ProjectileManager ProjectileManager { get; }
  internal IEnumerable<IEnemy> GetAliveEnemies();
  internal void RestoreEnemies(List<IEnemy> restoredEnemies);

  internal Vector2 GetDefaultPlayerPosition();
  internal bool HasKillableEnemiesRemaining();
  internal void KillAllDamageableEnemies();
  internal void AddPickup(IWorldPickup pickup);
  internal void RemovePickup(IWorldPickup pickup);
  internal IEnumerable<IBlock> GetOpenableDoors();
  internal IWorldPickup? GetClosestPickupInRange(Vector2 position, float range);
  internal void PlayerResolveCollisions(ICollidable movingEntity, CollisionAxis axis = CollisionAxis.Both, float cornerTolerance = 3.0f);
}
