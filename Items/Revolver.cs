using System.Collections.Generic;
using GameProject.Animations;
using GameProject.Interfaces;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

public class Revolver : IItem {
  private Texture2D texture;
  private Vector2 position;
  private Rectangle sourceRectangle = new Rectangle(0, 0, 8, 8);
  private Vector2 origin;

  private IProjectile bulletProjectile;
  private float bulletVelocity = 10f;
  private float bulletLifetime = 2f;

  public void Draw(SpriteBatch spriteBatch) {
    origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

    spriteBatch.Draw(
      texture,
      position,
      sourceRectangle,
      Color.White,
      0f,
      origin,
      1f,
      SpriteEffects.None,
      0f
    );
  }

  public void Update(GameTime gameTime) {
  }

  public void OnPickup() {

  }

  public void Use() {
    Vector2 bulletDirection = new Vector2(1, 0);
    bulletProjectile.Instantiate(position, bulletDirection, bulletVelocity, bulletLifetime);
  }

  public Revolver(Texture2D texture, Vector2 startPosition) {
    this.texture = texture;
    position = startPosition;
    bulletProjectile = new BulletDefault(texture);
    sourceRectangle = new Rectangle(0, 0, 8, 8);
  }
}
