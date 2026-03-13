using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameProject.Interfaces;

public interface ILevel : IInitable, IUpdatable, IGPDrawable {
  void AddPickup(IWorldPickup pickup);
  List<IBlock> CollidableBlocks { get; }
  List<IEnemy> Enemies { get; }
  Vector2 PlayerPosition { get; }
}
