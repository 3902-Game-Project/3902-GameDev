using System.Collections.Generic;
using GameProject.Animations;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Projectiles;

public class BombProjectile : IProjectile {
  private Texture2D texture;
  private Vector2 position;
  private List<Rectangle> sourceRectangles = new() {
        new Rectangle(16, 0, 5, 16),
        new Rectangle(22, 0, 5, 16),

    };
  private Rectangle currentSourceRect;
  private Animation bombAnimation;
  private Vector2 origin;
  public bool IsExpired { get; private set; } = false;

  public void Expire() {
    IsExpired = true;
  }

  public Rectangle BoundingBox {
    get {
      int width = currentSourceRect.Width;
      int height = currentSourceRect.Height;
      return new Rectangle((int) position.X - (width / 2), (int) position.Y - (height / 2), width, height);
    }
  }

  private Vector2 direction;
  private float velocity;
  private double lifetime;
  private double lifetimeCounter;


  public void Draw(SpriteBatch spriteBatch) {
    origin = new Vector2(currentSourceRect.Width / 2, currentSourceRect.Height / 2);

    spriteBatch.Draw(
        texture,
        position,
        currentSourceRect,
        Color.White,
        0f,
        origin,
        1f,
        SpriteEffects.None,
        0f
    );
  }

  public void Update(GameTime gameTime) {
    bombAnimation.Update(gameTime);
    currentSourceRect = bombAnimation.CurrentFrame;

    lifetimeCounter += gameTime.ElapsedGameTime.TotalSeconds;
    if (lifetimeCounter >= lifetime) {
      IsExpired = true;
    }
  }

  public BombProjectile(Texture2D texture, Vector2 startPosition, Vector2 direction, float velocity, float lifetime) {
    this.texture = texture;
    position = startPosition;
    this.direction = direction;
    this.velocity = velocity;
    this.lifetime = lifetime;
    lifetimeCounter = 0f;
    bombAnimation = new Animation(sourceRectangles, 10);
    currentSourceRect = bombAnimation.CurrentFrame;
  }
}
