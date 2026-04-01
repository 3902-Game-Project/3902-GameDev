using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Collisions;
public class BoxCollider(float width, float height, Vector2 position) : IShape {
  public ShapeType Type { get; } = ShapeType.Box;
  public float width = width;
  public float height = height;
  public Vector2 position { get; set; } = position;
  public float Left => position.X - (width * 0.5f);
  public float Right => position.X + (width * 0.5f);
  public float Top => position.Y - (height * 0.5f);
  public float Bottom => position.Y + (height * 0.5f);
}
