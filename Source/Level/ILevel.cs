using System.Collections.Generic;
using GameProject.Blocks;
using GameProject.Enemies;
using GameProject.GlobalInterfaces;
using GameProject.Managers;
using GameProject.PlayerSpace;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;

namespace GameProject.Level;

internal interface ILevel : IInitable, IGPUpdatable, IGPDrawable {
  List<IBlock> Doors { get; }
  List<IEnemy> Enemies { get; }
  Vector2 PlayerPosition { get; }
  public ProjectileManager ProjectileManager { get; }
  public CollisionManager CollisionManager { get; }
  List<IWorldPickup> Pickups { get; }

  void AddPickup(IWorldPickup pickup);
  void RemovePickup(IWorldPickup pickup);
  void LevelSwitchUpdateColliders(Player player);
}
