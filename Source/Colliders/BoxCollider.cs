using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Collisions;

public class BoxCollider : ICollider {
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
    if (other is BoxCollider box) {
      return CheckBoxCollision(box);
    } else if (other is CircleCollider circle) {
      return CheckCircleCollision(circle);
    }
    return false;
  }

  private bool CheckBoxCollision(BoxCollider other) {
    return this.Left < other.Right &&
           this.Right > other.Left &&
           this.Top < other.Bottom &&
           this.Bottom > other.Top;
  }

  private bool CheckCircleCollision(CircleCollider other) {
    return false;
  }
}
