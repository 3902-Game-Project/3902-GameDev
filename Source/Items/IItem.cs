using GameProject.Enums;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;

namespace GameProject.Interfaces;

public interface IItem : ISprite {
  ItemCategory Category { get; }
  FacingDirection Direction { get; set; }
  Vector2 Position { get; set; }
  public void OnPickup(Player player);
  void Use(UseType useType);
}
