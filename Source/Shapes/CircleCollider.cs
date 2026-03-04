using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Collisions;
public class CircleCollider : IShape {
  public ShapeType Type { get; } = ShapeType.Circle;
  public float radius;
  public Vector2 position;

  public CircleCollider(float radius, Vector2 position) {
    this.radius = radius;
    this.position = position;
  }

  public bool CheckCollision(ICollider other) {
    return false;
  }
}
