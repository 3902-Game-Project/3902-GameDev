using GameProject.Enums;
using Microsoft.Xna.Framework;

namespace GameProject.Interfaces;

public interface IItem : ISprite {

  ItemCategory Category { get; }
  Vector2 Position { get; set; }
  void Use(UseType useType);
}
