using System;
using System.Collections.Generic;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Sprites {
  public class SnakeSprite : IEnemy {
    private Texture2D texture;
    private Vector2 position;
    private Vector2 startPosition;
    private List<Rectangle> sourceRectangles;

    private int direction = -1;
    private int speed = 2;
    private int patrolDistance = 100;

    private int currentFrame;
    private double timer;
    private double frameInterval = 0.2;

    public SnakeSprite(Texture2D texture, Vector2 startPosition) {
      this.texture = texture;
      this.position = startPosition;
      this.startPosition = startPosition;
      this.timer = 0;
      this.currentFrame = 0;

      sourceRectangles = new List<Rectangle>();
      sourceRectangles.Add(new Rectangle(0, 0, 16, 16));
      sourceRectangles.Add(new Rectangle(16, 0, 16, 16)); 
    }

    public void Update(GameTime gameTime) {
      timer += gameTime.ElapsedGameTime.TotalSeconds;
      if (timer >= frameInterval) {
        currentFrame++;
        if (currentFrame >= sourceRectangles.Count)
          currentFrame = 0;
        timer = 0;
      }

      position.X += speed * direction;

      if (direction == -1 && position.X <= startPosition.X - patrolDistance) {
        direction = 1; 
      } else if (direction == 1 && position.X >= startPosition.X + patrolDistance) {
        direction = -1; 
      }
    }

    public void Draw(SpriteBatch spriteBatch) {
      Rectangle sourceRectangle = sourceRectangles[currentFrame];
      SpriteEffects effects = (direction == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

      spriteBatch.Draw(texture, position, sourceRectangle, Color.White, 0f, Vector2.Zero, 2.0f, effects, 0f);
    }

    public void ChangeDirection() { /* TODO */ }
    public void TakeDamage() { /* TODO */ }
  }
}
