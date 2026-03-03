using GameProject.Interfaces;
using Microsoft.Xna.Framework;

public class BoxCollider : ICollider {
  public Vector2 dimensions;
  public Vector2 position;
  public Vector2[] corners { get; private set; }

  public BoxCollider(Vector2 dimensions, Vector2 position) {
    this.dimensions = dimensions;
    this.position = position;
    corners = new Vector2[4];
    SetCorners();
  }

  public bool CheckCollision(ICollider other) {
    return false;
  }

  public void Update(GameTime gameTime) {
    
  }

  private void SetCorners() {
    corners[0] = new Vector2(position.X - dimensions.X * 0.5f, position.Y - dimensions.Y * 0.5f);
    corners[1] = new Vector2(position.X + dimensions.X * 0.5f, position.Y - dimensions.Y * 0.5f);
    corners[2] = new Vector2(position.X - dimensions.X * 0.5f, position.Y + dimensions.Y * 0.5f);
    corners[3] = new Vector2(position.X + dimensions.X * 0.5f, position.Y + dimensions.Y * 0.5f);
  }
}
