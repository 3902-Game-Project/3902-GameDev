using System;
using GameProject.Factories;
using GameProject.Interfaces;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

public class Shotgun : IItem {
  private Texture2D texture;
  private Vector2 position;
  private Rectangle sourceRectangle = new Rectangle(0, 0, 8, 8);
  private Vector2 origin;

  private ProjectileManager projectileManager;
  private float bulletVelocity = 10f;
  private float bulletLifetime = 0.5f;
  private int pelletCount = 5;
  private float spreadAngle = 30f;

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

  public void Update(GameTime gameTime) { }

  public void OnPickup() { }

  public void Use() {
    Vector2 bulletDirection = new Vector2(1, 0);
    for (int i = 0; i < pelletCount; i++) {
      float angle = -spreadAngle / 2 + spreadAngle / (pelletCount - 1) * i;
      Vector2 rotatedDirection = RotateVector(bulletDirection, angle);
      projectileManager.AddProjectile(ProjectileFactory.Instance.CreateBullet(position, rotatedDirection, bulletVelocity, bulletLifetime));
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

  public Shotgun(Texture2D texture, Vector2 startPosition, ProjectileManager projectileManager) {
    this.texture = texture;
    this.position = startPosition;
    this.projectileManager = projectileManager;
    sourceRectangle = new Rectangle(0, 0, 8, 8);
  }
}
