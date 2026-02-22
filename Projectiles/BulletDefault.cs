using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Projectiles;
public class BulletDefault : IProjectile {
  private Texture2D texture;
  private Vector2 position;
  private Rectangle sourceRectangle;
  private Vector2 origin;
  private Vector2 direction;
  private float velocity;
  private float bulletLifetime;
  private float lifetimeCounter;
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

  public BulletDefault(Texture2D texture, Vector2 startPosition, Vector2 direction, float velocity, float lifetime) {
    this.texture = texture;
    sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
    position = startPosition;
    this.direction = direction;
    this.velocity = velocity;
    bulletLifetime = lifetime;
    lifetimeCounter = 0f;
  }
}
