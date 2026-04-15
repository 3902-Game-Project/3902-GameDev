using System.Collections.Generic;
using GameProject.Blocks;
using GameProject.Collisions;
using GameProject.GlobalInterfaces;
using GameProject.Managers;
using GameProject.PlayerSpace;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;

namespace GameProject.Level;

internal interface ILevel : IInitable, IGPUpdatable, IGPDrawable {
  List<IWorldPickup> Pickups { get; }
  public ProjectileManager ProjectileManager { get; }

  Vector2 GetDefaultPlayerPosition();
  void AddPickup(IWorldPickup pickup);
  void RemovePickup(IWorldPickup pickup);
  IEnumerable<IBlock> GetOpenableDoors();
  void PlayerResolveCollisions(ICollidable movingEntity, CollisionAxis axis = CollisionAxis.Both, float cornerTolerance = 3.0f);
}
