using GameProject.GlobalInterfaces;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;

namespace GameProject.WorldPickups;

internal interface IWorldPickup : ISprite {
  internal Vector2 Position { get; }
  internal bool IsAutoCollect { get; }
  internal void OnPickup(Player player);
}
