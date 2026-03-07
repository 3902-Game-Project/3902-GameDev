using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Collisions;
public class BoxCollider : IShape, ICollider {
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


  public bool CheckCollision(ICollider other) {
    if (other is BoxCollider otherBox) {
      return this.Left < otherBox.Right &&
             this.Right > otherBox.Left &&
             this.Top < otherBox.Bottom &&
             this.Bottom > otherBox.Top;
    }
    return false;
  }
}
