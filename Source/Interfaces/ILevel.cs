using System.Collections.Generic;

namespace GameProject.Interfaces;

public interface ILevel : IInitable, IUpdatable, IGPDrawable {
  void AddPickup(IWorldPickup pickup);
  List<IBlock> CollidableBlocks { get; }
  List<IEnemy> Enemies { get; }
}
