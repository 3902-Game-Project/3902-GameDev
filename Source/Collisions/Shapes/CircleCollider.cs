using Microsoft.Xna.Framework;

namespace GameProject.Collisions.Shapes;

internal class CircleCollider(float radius, Vector2 position) : IShape {
  internal ShapeType Type { get; } = ShapeType.Circle;
  internal float Radius { get; } = radius;
  internal Vector2 Position { get; set; } = position;
}
