using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Collisions;
public class BoxCollider : IShape {
  public ShapeType Type { get; } = ShapeType.Box;
  public float width;
  public float height;
  public Vector2 position { get; set; }
  public float Left => position.X - (width * 0.5f);
  public float Right => position.X + (width * 0.5f);
  public float Top => position.Y - (height * 0.5f);
  public float Bottom => position.Y + (height * 0.5f);

  public BoxCollider(float width, float height, Vector2 position) {
    this.width = width;
    this.height = height;
    this.position = position;
  }
}
