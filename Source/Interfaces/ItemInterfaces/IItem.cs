namespace GameProject.Interfaces;

public enum UseType { Pressed, Held, Released }

public interface IItem : ISprite {
  void Use(UseType useType);
}
