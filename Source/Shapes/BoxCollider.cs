using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Collisions;
public class BoxCollider : IShape, ICollider {
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

  // added the removed constructor to fix compile errors:
  public BoxCollider(Vector2 dimensions, Vector2 position) {
    width = dimensions.X;
    height = dimensions.Y;
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
