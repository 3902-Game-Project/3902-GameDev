using GameProject.Collisions;
using GameProject.GlobalInterfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies;

internal interface IEnemy : ISprite, ICollidable {
  void TakeDamage(int damage);
  int Health { get; }
  int MaxHealth { get; }

  Rectangle BoundingBox { get; }
  Vector2 Velocity { get; set; }
  int FacingDirection { get; set; }
}
