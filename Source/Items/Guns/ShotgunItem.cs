using GameProject.Interfaces;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

public class ShotgunItem : IItem {
  private Vector2 position;
  private Rectangle sourceRectangle = new(0, 10, 27, 9);
  private Vector2 origin;
  private Texture2D texture;
  private float scale = 3f;

  private ProjectileManager projectileManager;
  private IProjectilePattern projectilePattern = new SpreadPattern();
  private Vector2 bulletSpawnOffset;
  private GunStats stats;

  public ShotgunItem(Texture2D texture, Vector2 position, ProjectileManager projectileManager, GunStats stats) {
    this.projectileManager = projectileManager;
    this.texture = texture;
    this.position = position;
    this.stats = stats;
    bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2, -1 * (sourceRectangle.Height / 2 - 3)) * scale; // Adjust spawn offset based on the shotgun's size and scale
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

  public void Use(UseType useType) {
    Vector2 bulletDirection = new(1, 0);
    Vector2 bulletSpawnPosition = position + bulletSpawnOffset;
    projectilePattern.SpawnProjectiles(projectileManager, bulletSpawnPosition, bulletDirection, stats);
  }
}
