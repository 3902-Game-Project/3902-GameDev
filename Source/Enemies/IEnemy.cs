using GameProject.Collisions;
using GameProject.Collisions.Shapes;
using GameProject.GlobalInterfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies;

internal enum FacingDirection {
  Left,
  Right,
  Up,
  Down,
}

internal interface IEnemy : ISprite, ICollidable {
  int Health { get; }
  int MaxHealth { get; }

  Rectangle BoundingBox { get; }
  Vector2 Velocity { get; set; }
  Vector2 Target { get; set; }
  FacingDirection Direction { get; set; }
  BoxCollider Collider { get; }
  bool Invulnerable { get; }

  void TakeDamage(int damage);
  void Kill();
}
