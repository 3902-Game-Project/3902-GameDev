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
    private int speed = 0;
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

      Vector2 origin = new Vector2(0, sourceRectangle.Height);

      spriteBatch.Draw(
          texture: texture,
          position: position,
          sourceRectangle: sourceRectangle,
          color: Color.White,
          rotation: 0f,
          origin: origin, // <--- Use the new origin here
          scale: 2f,      // If you scale by 2, the gap doubles, so Origin fixes this automatically!
          effects: effects,
          layerDepth: 0f
          );
    }

    public void ChangeDirection() { /* TODO */ }
    public void TakeDamage() { /* TODO */ }
  }
}
