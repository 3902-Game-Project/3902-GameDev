using System.Collections.Generic;
using GameProject.Animations;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

public class AmmoRefill : IItem {
  private Texture2D texture;
  private Vector2 position;
  private List<Rectangle> sourceRectangles;
  private Rectangle currentSourceRect;
  private Vector2 origin;

  private Animation shootAnimation;

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
    shootAnimation.Update(gameTime);
    currentSourceRect = shootAnimation.CurrentFrame;
  }

  public void OnPickup() {
    // Logic for using the ammo refill item
  }

  public AmmoRefill(Texture2D texture, Vector2 startPosition) {
    this.texture = texture;
    this.position = startPosition;
    shootAnimation = new Animation(sourceRectangles, 10);
    currentSourceRect = shootAnimation.CurrentFrame;
  }
}
