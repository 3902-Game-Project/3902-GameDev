using GameProject.PlayerSpace;
namespace GameProject.Interfaces;

public interface IWorldPickup : ISprite {
  void OnPickup(Player player);
}
