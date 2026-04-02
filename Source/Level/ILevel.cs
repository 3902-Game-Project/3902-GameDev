using System.Collections.Generic;
using GameProject.Managers;
using Microsoft.Xna.Framework;

namespace GameProject.Interfaces;

public interface ILevel : IInitable, IUpdatable, IGPDrawable {
  List<IBlock> CollidableBlocks { get; }
  List<IEnemy> Enemies { get; }
  Vector2 PlayerPosition { get; }
  public ProjectileManager ProjectileManager { get; }
  void AddPickup(IWorldPickup pickup);
  void FadeIn();
  void FadeOut();
  bool IsFadingOut();
  void RemovePickup(IWorldPickup pickup);
  List<IWorldPickup> Pickups { get; }
}
