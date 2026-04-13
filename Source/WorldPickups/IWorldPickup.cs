using GameProject.PlayerSpace;
namespace GameProject.Interfaces;

internal interface IWorldPickup : ISprite {
  void OnPickup(Player player);
}
