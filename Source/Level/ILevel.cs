using System.Collections.Generic;
using GameProject.Blocks;
using GameProject.Collisions;
using GameProject.GlobalInterfaces;
using GameProject.Managers;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;

namespace GameProject.Level;

#nullable enable

internal interface ILevel : IInitable, ITemporalUpdatable, IGPDrawable {
  public ProjectileManager ProjectileManager { get; }

  Vector2 GetDefaultPlayerPosition();
  void AddPickup(IWorldPickup pickup);
  void RemovePickup(IWorldPickup pickup);
  IEnumerable<IBlock> GetOpenableDoors();
  IEnumerable<IWorldPickup> GetRemoveAmmoInRange(Vector2 position, float range);
  public IWorldPickup? GetClosestPickupInRange(Vector2 position, float range);
  void PlayerResolveCollisions(ICollidable movingEntity, CollisionAxis axis = CollisionAxis.Both, float cornerTolerance = 3.0f);
}
