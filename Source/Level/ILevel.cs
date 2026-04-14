using System.Collections.Generic;
using GameProject.Blocks;
using GameProject.Enemies;
using GameProject.GlobalInterfaces;
using GameProject.Managers;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;

namespace GameProject.Level;

internal interface ILevel : IInitable, IGPUpdatable, IGPDrawable {
  List<IBlock> Doors { get; }
  List<IBlock> CollidableBlocks { get; }
  List<IEnemy> Enemies { get; }
  Vector2 PlayerPosition { get; }
  public ProjectileManager ProjectileManager { get; }
  public CollisionManager CollisionManager { get; }
  void AddPickup(IWorldPickup pickup);
  void RemovePickup(IWorldPickup pickup);
  List<IWorldPickup> Pickups { get; }
}
