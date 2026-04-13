using GameProject.PlayerSpace;

namespace GameProject.WorldPickups;

internal interface IWorldPickup : ISprite {
  void OnPickup(Player player);
}
