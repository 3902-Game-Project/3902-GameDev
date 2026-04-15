using System.Collections.Generic;
using GameProject.Blocks;
using GameProject.GlobalInterfaces;
using GameProject.Managers;
using GameProject.PlayerSpace;
using GameProject.WorldPickups;
using Microsoft.Xna.Framework;

namespace GameProject.Level;

internal interface ILevel : IInitable, IGPUpdatable, IGPDrawable {
  List<IWorldPickup> Pickups { get; }
  public ProjectileManager ProjectileManager { get; }
  public CollisionManager CollisionManager { get; }

  Vector2 GetDefaultPlayerPosition();
  void AddPickup(IWorldPickup pickup);
  void RemovePickup(IWorldPickup pickup);
  void LevelSwitchUpdateColliders(Player player);
  IEnumerable<IBlock> GetOpenableDoors();
}
