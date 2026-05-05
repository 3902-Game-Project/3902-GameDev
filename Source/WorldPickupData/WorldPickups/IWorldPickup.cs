using GameProject.GlobalInterfaces;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;

namespace GameProject.WorldPickups;

internal interface IWorldPickup : ISprite {
  Vector2 Position { get; }
  bool IsAutoCollect { get; }
  void OnPickup(Player player);
}
