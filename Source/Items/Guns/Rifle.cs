using GameProject.Factories;
using GameProject.Interfaces;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items.Guns;

public class Rifle : IItem {
  private Rectangle sourceRectangle = new(0, 19, 37, 10);
  private Vector2 origin;
  private float scale = 3f;
  private Texture2D texture;
  private Vector2 position = new(300, 300);

  private ProjectileManager projectileManager;
  private Vector2 bulletSpawnOffset;
  private GunStats stats;

  public Rifle(Texture2D texture, Vector2 startPosition, ProjectileManager projectileManager, GunStats stats) {
    this.projectileManager = projectileManager;
    this.texture = texture;
    this.position = startPosition;
    this.stats = stats;
    this.bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2, -1 * (sourceRectangle.Height / 2 - 3)) * scale;
  }

  public void Draw(SpriteBatch spriteBatch) {
    origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

    spriteBatch.Draw(
      texture,
      position,
      sourceRectangle,
      Color.White,
      0f,
      origin,
      scale,
      SpriteEffects.None,
      0f
    );
  }

  public void Update(GameTime gameTime) { }

  public void OnPickup() { }

  public void Use() {
    Vector2 bulletDirection = new(1, 0);
    Vector2 bulletSpawnPosition = position + bulletSpawnOffset;
    projectileManager.AddProjectile(ProjectileFactory.Instance.CreateBullet(bulletSpawnPosition, bulletDirection, stats.BulletVelocity, stats.BulletLifetime));
  }
}
