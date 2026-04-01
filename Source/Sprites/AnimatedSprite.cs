using System.Collections.Generic;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Sprites;

internal class AnimatedSprite : ISprite {
  private Texture2D texture;
  private Vector2 position;

  private List<Rectangle> sourceRectangles;

  private int currentFrame;
  private double timer;
  private double FrameInterval = 0.2; // How long we want to wait to change the frame of a sprite

  public AnimatedSprite(Texture2D texture, Vector2 position) {

    this.texture = texture;
    this.position = position;
    currentFrame = 0;
    timer = 0;

    sourceRectangles = new List<Rectangle>();
    sourceRectangles.Add(new Rectangle(0, 0, 23, 25));
    sourceRectangles.Add(new Rectangle(22, 0, 23, 25));
    sourceRectangles.Add(new Rectangle(44, 0, 21, 25));
  }

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
