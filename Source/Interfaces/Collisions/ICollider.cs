using Microsoft.Xna.Framework;

namespace GameProject.Interfaces;

public enum Layer { Player, Environment, Enemies, Projectiles }

public interface ICollider {
  IShape Shape { get; }
  Vector2 Position { get; }
  Layer Layer { get; }
  Layer Mask { get; }

  void OnCollision(ICollider other);
}
