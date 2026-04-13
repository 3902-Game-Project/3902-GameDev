using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Collisions;
internal class CircleCollider(float radius, Vector2 position) : IShape {
  public ShapeType Type { get; } = ShapeType.Circle;
  public float Radius { get; } = radius;
  public Vector2 Position { get; set; } = position;
}
