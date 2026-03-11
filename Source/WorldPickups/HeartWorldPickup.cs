using GameProject.Collisions;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.WorldPickups;

public class HeartWorldPickup : IWorldPickup, ICollidable {
private Texture2D texture;
  private Rectangle sourceRectangle = new(9, 15, 12, 11);
  private Vector2 origin;
  private float scale = 2f;

  public Vector2 Position { get; }
  public IShape Shape { get; }
  public Layer Layer { get; } = Layer.Pickups;
  public Layer Mask { get; }= Layer.Player;

  public HeartWorldPickup(Texture2D texture, Vector2 startPosition) {
    this.texture = texture;
    Position = startPosition;
    Shape = new CircleCollider(10, Position);
  }

  public void Draw(SpriteBatch spriteBatch) {
    origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

    spriteBatch.Draw(
      texture,
      Position,
      sourceRectangle,
      Color.White,
      0f,
      origin,
      scale,
      SpriteEffects.None,
      0f
    );
  }

  public void Update(GameTime gameTime) {

  }

  public void OnPickup(Player player) {

  }

  public void OnCollision(CollisionInfo info) {
    if (info.Collider.Layer == Layer.Player && info.Collider as Player != null) {
      OnPickup(info.Collider as Player);
    }
  }
}
