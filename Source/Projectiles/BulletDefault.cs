using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Projectiles;

public class BulletDefault(Texture2D texture, Vector2 startPosition, Vector2 direction, float velocity, float bulletLifetime) : IProjectile {
  private Vector2 position = startPosition;
  private Rectangle sourceRectangle = new(8, 0, 7, 7);
  private Vector2 origin;
  private float scale = 2f;
  private Vector2 direction = direction;
  private float lifetimeCounter = 0f;
  public bool IsExpired { get; private set; }

  public void Expire() {
    IsExpired = true;
  }

  public Rectangle BoundingBox {
    get {
      int width = (int)(sourceRectangle.Width * scale);
      int height = (int)(sourceRectangle.Height * scale);
      int x = (int)position.X - (width / 2);
      int y = (int)position.Y - (height / 2);

      return new Rectangle(x, y, width, height);
    }
  }

  public void Draw(SpriteBatch spriteBatch) {
    origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

    spriteBatch.Draw(
      texture,
      position,
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
    // Logic for updating the bullet's position and state
    position += direction * velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    lifetimeCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
    if (lifetimeCounter >= bulletLifetime) {
      // Logic for destroying the bullet
    }
  }
}
