using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Collisions;
internal class BoxCollider(float width, float height, Vector2 position) : IShape {
  public ShapeType Type { get; } = ShapeType.Box;
  public float Width { get; } = width;
  public float Height { get; } = height;
  public Vector2 Position { get; set; } = position;
  public float Left => Position.X - (Width * 0.5f);
  public float Right => Position.X + (Width * 0.5f);
  public float Top => Position.Y - (Height * 0.5f);
  public float Bottom => Position.Y + (Height * 0.5f);
}
