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
  internal int Health { get; }
  internal int MaxHealth { get; }

  internal Vector2 Velocity { get; set; }
  internal Vector2 Target { get; set; }
  internal FacingDirection Direction { get; set; }
  internal BoxCollider Collider { get; }
  internal bool Invulnerable { get; }

  internal void TakeDamage(int damage);
  internal void Kill();
}
