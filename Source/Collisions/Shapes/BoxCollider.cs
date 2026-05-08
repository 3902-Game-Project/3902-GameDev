using Microsoft.Xna.Framework;

namespace GameProject.Collisions.Shapes;

internal class BoxCollider(float width, float height, Vector2 position) : IShape {
  internal ShapeType Type { get; } = ShapeType.Box;
  internal float Width { get; } = width;
  internal float Height { get; } = height;
  internal Vector2 Position { get; set; } = position;
  internal float Left => Position.X - (Width * 0.5f);
  internal float Right => Position.X + (Width * 0.5f);
  internal float Top => Position.Y - (Height * 0.5f);
  internal float Bottom => Position.Y + (Height * 0.5f);
}
