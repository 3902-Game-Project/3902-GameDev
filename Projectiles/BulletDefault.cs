using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Projectiles;

public class BulletDefault(Texture2D texture, Vector2 startPosition, Vector2 direction, float velocity, float bulletLifetime) : IProjectile {
  private Vector2 position = startPosition;
  private Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
  private Vector2 origin;
  private Vector2 direction = direction;
  private float lifetimeCounter = 0f;
  public bool IsExpired { get; private set; }

  public void Draw(SpriteBatch spriteBatch) {
    origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

    spriteBatch.Draw(
      texture,
      position,
      sourceRectangle,
      Color.White,
      0f,
      origin,
      1f,
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
