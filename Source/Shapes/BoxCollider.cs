using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Collisions;
public class BoxCollider : IShape {
  public ShapeType Type { get; } = ShapeType.Box;
  public Vector2 dimensions { get; set; }
  public Vector2 position { get; set; } 
  public float Left => position.X - (dimensions.X * 0.5f);
  public float Right => position.X + (dimensions.X * 0.5f);
  public float Top => position.Y - (dimensions.Y * 0.5f);
  public float Bottom => position.Y + (dimensions.Y * 0.5f);

  public BoxCollider(Vector2 dimensions, Vector2 position) {
    this.dimensions = dimensions;
    this.position = position;
  }
}
