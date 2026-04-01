using System.Collections.Generic;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Sprites;

public class LeftAndRightAnimatedSprite(Texture2D texture, Vector2 position) : ISprite {
  private Vector2 startPosition = position;

  private List<Rectangle> sourceRectangles =
    [
      new Rectangle(65, 0, 25, 25),
      new Rectangle(90, 0, 17, 25),
      new Rectangle(108, 0, 22, 25),
    ];

  private int direction = -1;
  private int speed = 3;
  private int sprintLength = 100;

  private int currentFrame = 0;
  private double timer = 0;
  private double FrameInterval = 0.2;

  public void Update(GameTime gameTime) {
    timer += gameTime.ElapsedGameTime.TotalSeconds;
    position.X += speed * direction;

    if (direction == -1 && position.X <= startPosition.X - sprintLength) {
      direction = 1;
    } else if (direction == 1 && position.X >= startPosition.X + 100) {
      direction = -1;
    }

    if (timer >= FrameInterval) {
      currentFrame++;
      if (currentFrame >= sourceRectangles.Count) {
        currentFrame = 0;
      }
      timer = 0;
    }
  }

  public void Draw(SpriteBatch spriteBatch) {
    Rectangle sourceRectangle = sourceRectangles[currentFrame];
    SpriteEffects effects = (direction == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

    spriteBatch.Draw(
      texture: texture,
      position: position,
      sourceRectangle: sourceRectangle,
      color: Color.White,
      rotation: 0f,
      origin: Vector2.Zero,
      scale: 1f,
      effects: effects,
      layerDepth: 0f
    );
  }
}
