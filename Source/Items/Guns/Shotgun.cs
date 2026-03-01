using System;
using GameProject.Factories;
using GameProject.Interfaces;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items.Guns;

public class Shotgun : IItem {
  private Vector2 position;
  private Rectangle sourceRectangle = new(0, 10, 27, 9);
  private Vector2 origin;
  private Texture2D texture;
  private float scale = 3f;

  private ProjectileManager projectileManager;
  private Vector2 bulletSpawnOffset;
  private GunStats stats;

  public Shotgun(Texture2D texture, Vector2 position, ProjectileManager projectileManager, GunStats stats) {
    this.projectileManager = projectileManager;
    this.texture = texture;
    this.position = position;
    this.stats = stats;
    this.bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2, -1 * (sourceRectangle.Height / 2 - 3)) * scale; // Adjust spawn offset based on the shotgun's size and scale
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
    for (int i = 0; i < stats.PelletCount; i++) {
      float angle = -stats.SpreadAngle / 2 + stats.SpreadAngle / (stats.PelletCount - 1) * i;
      Vector2 rotatedDirection = RotateVector(bulletDirection, angle);
      projectileManager.AddProjectile(ProjectileFactory.Instance.CreateBullet(bulletSpawnPosition, rotatedDirection, stats.BulletVelocity, stats.BulletLifetime));
    }
  }

  private Vector2 RotateVector(Vector2 vector, float angleDegrees) {
    float angleRadians = MathHelper.ToRadians(angleDegrees);
    float cos = (float)Math.Cos(angleRadians);
    float sin = (float)Math.Sin(angleRadians);
    return new Vector2(
      vector.X * cos - vector.Y * sin,
      vector.X * sin + vector.Y * cos
    );
  }
}
