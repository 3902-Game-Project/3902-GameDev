using System.Collections.Generic;
using GameProject.Animations;
using GameProject.Interfaces;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

public class Rifle : IItem {
  private Texture2D texture;
  private Vector2 position;
  private List<Rectangle> sourceRectangles;
  private Animation rifleAnimation;
  private Rectangle currentSourceRect;
  private Vector2 origin;

  private IProjectile bulletProjectile;
  private float bulletVelocity = 25f;

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
    rifleAnimation.Update(gameTime);
    currentSourceRect = rifleAnimation.CurrentFrame;
  }

  public void OnPickup() {

  }

  public void Use() {
    Vector2 bulletDirection = new Vector2(1, 0);
    bulletProjectile.Instantiate(position, bulletDirection, bulletVelocity);
  }

  public Rifle(Texture2D texture, Vector2 startPosition) {
    this.texture = texture;
    this.position = startPosition;
    rifleAnimation = new Animation(sourceRectangles, 10);
    currentSourceRect = rifleAnimation.CurrentFrame;
    bulletProjectile = new BulletDefault(texture);
  }
}
