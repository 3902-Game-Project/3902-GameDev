using GameProject.Factories;
using GameProject.Interfaces;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

public class Rifle(Texture2D texture, Vector2 startPosition, ProjectileManager projectileManager) : IItem {
  private Rectangle sourceRectangle = new(0, 19, 37, 10);
  private Vector2 origin;

  private ProjectileManager projectileManager = projectileManager;
  private float bulletVelocity = 25f;
  private float bulletLifetime = 2f;

  public void Draw(SpriteBatch spriteBatch) {
    origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

    spriteBatch.Draw(
      texture,
      startPosition,
      sourceRectangle,
      Color.White,
      0f,
      origin,
      1f,
      SpriteEffects.None,
      0f
    );
  }

  public void Update(GameTime gameTime) { }

  public void OnPickup() { }

  public void Use() {
    Vector2 bulletDirection = new(1, 0);
    projectileManager.AddProjectile(ProjectileFactory.Instance.CreateBullet(startPosition, bulletDirection, bulletVelocity, bulletLifetime));
  }
}
