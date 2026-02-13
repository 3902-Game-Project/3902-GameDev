using System.Collections.Generic;
using GameProject.Animations;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

public class WhipDefault : IItem {
  private Texture2D texture;
  private Vector2 position;
  private List<Rectangle> sourceRectangles;
  private Rectangle currentSourceRect;
  private Animation whipAnimation;
  private Vector2 origin;

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
    whipAnimation.Update(gameTime);
    currentSourceRect = whipAnimation.CurrentFrame;
  }

  public void OnPickup() {
    // Logic for using the whip item
  }

  public void Use() {
    // Logic for using the whip item
  }

  public WhipDefault(Texture2D texture, Vector2 startPosition) {
    this.texture = texture;
    this.position = startPosition;
    whipAnimation = new Animation(sourceRectangles, 10);
    currentSourceRect = whipAnimation.CurrentFrame;
  }
}
