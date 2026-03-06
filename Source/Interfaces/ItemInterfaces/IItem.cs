using GameProject.Enums;

namespace GameProject.Interfaces;

public interface IItem : ISprite {

  ItemCategory Category { get; }
  void Use(UseType useType);
}
