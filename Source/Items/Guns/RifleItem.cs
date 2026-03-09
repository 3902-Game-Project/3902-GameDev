using GameProject.Enums;
using GameProject.Interfaces;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

public class RifleItem : IItem {
  private Rectangle sourceRectangle = new(0, 19, 37, 10);
  private Vector2 origin;
  private float scale = 3f;
  private Texture2D texture;
  public Vector2 Position { get; set; }

  private ProjectileManager projectileManager;
  private IProjectilePattern projectilePattern = new SingleShotPattern();
  private Vector2 bulletSpawnOffset;
  private GunStats stats;
  public ItemCategory Category { get; } = ItemCategory.Primary;

  public RifleItem(Texture2D texture, Vector2 startPosition, ProjectileManager projectileManager, GunStats stats) {
    this.projectileManager = projectileManager;
    this.texture = texture;
    this.Position = startPosition;
    this.stats = stats;
    this.bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2, -1 * (sourceRectangle.Height / 2 - 3)) * scale;
  }

  public void Draw(SpriteBatch spriteBatch) {
    origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

    spriteBatch.Draw(
      texture,
      Position,
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
    Vector2 bulletSpawnPosition = Position + bulletSpawnOffset;
    projectilePattern.SpawnProjectiles(projectileManager, bulletSpawnPosition, bulletDirection, stats);
  }
}
