using Microsoft.Xna.Framework;

namespace GameProject.Interfaces;

public interface IEnemy : ISprite, ICollidable {
  void TakeDamage();

  Rectangle BoundingBox { get; }
  Vector2 Velocity { get; set; }
  int FacingDirection { get; set; }
}
