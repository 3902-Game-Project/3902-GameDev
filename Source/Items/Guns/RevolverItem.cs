using GameProject.Enums;
using GameProject.Interfaces;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

public class RevolverItem : IItem {
  public FacingDirection Direction { get; set; } = FacingDirection.Right;
  private Rectangle sourceRectangle = new(0, 0, 16, 9);
  private Vector2 origin;
  private Texture2D texture;
  private float scale = 1.5f;
  public Vector2 Position { get; set; }

  private ProjectileManager projectileManager;
  private GunStats stats;
  private IProjectilePattern projectilePattern = new SingleShotPattern();
  private IFireMode fireMode;
  private Vector2 bulletSpawnOffset;
  public ItemCategory Category { get; } = ItemCategory.Sidearm;

  public RevolverItem(Texture2D texture, Vector2 startPosition, ProjectileManager projectileManager, GunStats stats) {
    this.projectileManager = projectileManager;
    this.texture = texture;
    this.stats = stats;
    fireMode = new SemiAutoFire(stats);
    Position = startPosition;
    bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2, -1 * (sourceRectangle.Height / 2 - 3)) * scale;
  }

  public void Draw(SpriteBatch spriteBatch) {
    origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

    SpriteEffects effects = SpriteEffects.None;
    if(Direction == FacingDirection.Left) {
      effects = SpriteEffects.FlipHorizontally;
    }

    spriteBatch.Draw(
      texture,
      Position,
      sourceRectangle,
      Color.White,
      0f,
      origin,
      scale,
      effects,
      0f
    );
  }

  public void Update(GameTime gameTime) {
    fireMode.Update(gameTime);
  }

  public void Use(UseType useType) {
    Vector2 bulletDirection;
    if (Direction == FacingDirection.Left) {
      bulletDirection = new Vector2(-1, 0);
    } else {
      bulletDirection = new Vector2(1, 0);
    }
    float offsetX;
    if (Direction == FacingDirection.Left) {
      offsetX = -bulletSpawnOffset.X;
    } else {
      offsetX = bulletSpawnOffset.X;
    }
    Vector2 actualOffset = new Vector2(offsetX, bulletSpawnOffset.Y);
    Vector2 bulletSpawnPosition = Position + actualOffset;
    if (fireMode.CanFire(useType)) {
      projectilePattern.SpawnProjectiles(projectileManager, bulletSpawnPosition, bulletDirection, stats);
    }
  }
}
