using System.Collections.Generic;
using GameProject.Interfaces;
using GameProject.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Bomb : IItem {
  private Texture2D texture;
  private Vector2 position;
  private List<Rectangle> sourceRectangles;
  private Rectangle currentSourceRect;
  private Animation bombAnimation;
  private Vector2 origin;

  private float fuseTime;

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

    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
    fuseTime -= dt;
    if (fuseTime <= 0) {
      Explode();
    }
  }

  public void OnPickup() {
    // Logic for using the bomb item
  }

  private void Explode() {
    
  }

  public Bomb(Texture2D texture, Vector2 startPosition) {
    this.texture = texture;
    this.position = startPosition;
    bombAnimation = new Animation(sourceRectangles, 5);
    currentSourceRect = bombAnimation.CurrentFrame;
  }
}