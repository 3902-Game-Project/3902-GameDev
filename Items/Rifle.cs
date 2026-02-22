using GameProject.Factories;
using GameProject.Interfaces;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

public class Rifle : IItem {
  private Texture2D texture;
  private Vector2 position;
  private Rectangle sourceRectangle;
  private Vector2 origin;

  private ProjectileManager projectileManager;
  private float bulletVelocity = 25f;
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

  public void Update(GameTime gameTime) { }

  public void OnPickup() { }

  public void Use() {
    Vector2 bulletDirection = new(1, 0);
    projectileManager.AddProjectile(ProjectileFactory.Instance.CreateBullet(position, bulletDirection, bulletVelocity, bulletLifetime));
  }

  public Rifle(Texture2D texture, Vector2 startPosition, ProjectileManager projectileManager) {
    this.texture = texture;
    this.position = startPosition;
    sourceRectangle = new Rectangle(0, 0, 8, 8);
    this.projectileManager = projectileManager;
  }
}
