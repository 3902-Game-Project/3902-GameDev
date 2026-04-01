using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Collisions;
public class CircleCollider(float radius, Vector2 position) : IShape {
  public ShapeType Type { get; } = ShapeType.Circle;
  public float radius = radius;
  public Vector2 position = position;
}
