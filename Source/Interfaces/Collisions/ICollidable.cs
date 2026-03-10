using GameProject.Collisions;
using Microsoft.Xna.Framework;

namespace GameProject.Interfaces;

public enum Layer { Player, Environment, Enemies, Projectiles }

public interface ICollidable {
  IShape Shape { get; }
  Vector2 Position { get; }
  Layer Layer { get; }
  Layer Mask { get; }

  void OnCollision(CollisionInfo collisionInfo);
}
