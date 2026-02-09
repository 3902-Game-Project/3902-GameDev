using System;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.ExceptionServices;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Sprites {
  public class SnakeSprite : IEnemy {
    private Texture2D texture;
    private Vector2 position;
    private List<Rectangle> sourceRectangles;

    private Vector2 velocity;
    private float speed = 100f;
    private Random random;
    private double directionTimer;

    private int currentFrame;
    private double animationTimer;

    public SnakeSprite(Texture2D texture, Vector2 startPosition) {

      this.texture = texture;
      this.position = startPosition;
      this.random = new Random();

      ChangeDirection();

      sourceRectangles = new List<Rectangle>();
      // Rectangle(x, y, width, height)
      sourceRectangles.Add(new Rectangle(11, 20, 10, 12));
      sourceRectangles.Add(new Rectangle(43, 21, 10, 11));
      sourceRectangles.Add(new Rectangle(75, 22, 10, 10));
      sourceRectangles.Add(new Rectangle(106, 22, 11, 10));
      sourceRectangles.Add(new Rectangle(138, 22, 11, 10));
      sourceRectangles.Add(new Rectangle(170, 22, 11, 10));
      sourceRectangles.Add(new Rectangle(203, 22, 10, 10));
      sourceRectangles.Add(new Rectangle(235, 21, 10, 11));
      sourceRectangles.Add(new Rectangle(267, 20, 10, 12));
      sourceRectangles.Add(new Rectangle(299, 20, 10, 12));
    }

    public void Update(GameTime gameTime) {
      float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

      animationTimer += dt;
      if (animationTimer >= 0.2)
      {
        currentFrame++;
        if (currentFrame >= sourceRectangles.Count) currentFrame = 0;
        animationTimer = 0;
      }

      position += velocity * dt;

      directionTimer -= dt;
      if(directionTimer <= 0) 
      {
        ChangeDirection();
        directionTimer = 2.0;
      }

      if (position.X < 0 || position.X > 800) velocity.X *= -1;
      if (position.Y < 0 || position.Y > 480) velocity.Y *= -1;
    }

    public void ChangeDirection() {
      int dir = random.Next(0, 4);

      switch (dir) {
        case 0: velocity = new Vector2(speed, 0); break;  // Right
        case 1: velocity = new Vector2(-speed, 0); break; // Left
        case 2: velocity = new Vector2(0, speed); break;  // Down
        case 3: velocity = new Vector2(0, -speed); break; // Up
      }
    }

    public void Draw(SpriteBatch spriteBatch) {
      Rectangle sourceRectangle = sourceRectangles[currentFrame];
      SpriteEffects effect = (velocity.X < 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

      Vector2 origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height);

      spriteBatch.Draw(
          texture: texture,
          position: position,
          sourceRectangle: sourceRectangle,
          color: Color.White,
          rotation: 0f,
          origin: origin,
          scale: 2f,
          effects: effect,
          layerDepth: 0f
          );
    }
    public void TakeDamage() { /* TODO */ }
  }
}
