using System.Collections.Generic;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Sprites;

internal class AnimatedSprite(Texture2D texture, Vector2 position) : ISprite {
  private List<Rectangle> sourceRectangles =
    [
      new Rectangle(0, 0, 23, 25),
      new Rectangle(22, 0, 23, 25),
      new Rectangle(44, 0, 21, 25),
    ];

  private int currentFrame = 0;
  private double timer = 0;
  private double FrameInterval = 0.2; // How long we want to wait to change the frame of a sprite

  public void Update(GameTime gameTime) {
    timer += gameTime.ElapsedGameTime.TotalSeconds;

    if (timer > FrameInterval) {
      currentFrame++;
      if (currentFrame >= sourceRectangles.Count) {
        currentFrame = 0;
      }
      timer = 0;
    }
  }

  public void Draw(SpriteBatch spriteBatch) {
    Rectangle sourceRectangle = sourceRectangles[currentFrame];

    spriteBatch.Draw(
      texture: texture,
      position: position,
      sourceRectangle: sourceRectangle,
      color: Color.White
    );
  }
}
