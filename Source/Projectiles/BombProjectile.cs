using System.Collections.Generic;
using GameProject.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Projectiles;

internal class BombProjectile : IProjectile {
  private static readonly List<Rectangle> SOURCE_RECTS = [
    new Rectangle(16, 0, 5, 16),
    new Rectangle(22, 0, 5, 16),
  ];

  private readonly Texture2D texture;
  private Vector2 position;
  private Rectangle currentSourceRect;
  private readonly Animation bombAnimation;
  private Vector2 origin;

  private readonly double lifetime;
  private double lifetimeCounter;

  internal bool IsExpired { get; private set; } = false;

  internal void Expire() {
    IsExpired = true;
  }

  internal Rectangle BoundingBox {
    get {
      int width = currentSourceRect.Width;
      int height = currentSourceRect.Height;
      return new Rectangle((int) position.X - (width / 2), (int) position.Y - (height / 2), width, height);
    }
  }

  internal void Draw(SpriteBatch spriteBatch) {
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

  internal void Update(double deltaTime) {
    bombAnimation.Update(deltaTime);
    currentSourceRect = bombAnimation.CurrentFrame;

    lifetimeCounter += deltaTime;
    if (lifetimeCounter >= lifetime) {
      IsExpired = true;
    }
  }

  internal BombProjectile(Texture2D texture, Vector2 startPosition, float lifetime) {
    this.texture = texture;
    position = startPosition;
    this.lifetime = lifetime;
    lifetimeCounter = 0f;
    bombAnimation = new Animation(SOURCE_RECTS, 10);
    currentSourceRect = bombAnimation.CurrentFrame;
  }
}
