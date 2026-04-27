using GameProject.Collisions;
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
  bool Invulnerable { get; }

  void TakeDamage(int damage);
}
