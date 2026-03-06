using System.Diagnostics;
using GameProject.Enums;
using GameProject.Factories;
using GameProject.Interfaces;
using GameProject.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

public class RevolverItem : IItem {
  private Rectangle sourceRectangle = new(0, 0, 16, 9);
  private Vector2 origin;
  private Texture2D texture;
  private float scale = 3f;
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
    bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2, -1 *(sourceRectangle.Height / 2 - 3)) * scale;
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

  public void Update(GameTime gameTime) {
    fireMode.Update(gameTime);
    Debug.WriteLine("Update Revolver");
  }

  public void Use(UseType useType) {
    Vector2 bulletDirection = new(1, 0);
    Vector2 bulletSpawnPosition = Position + bulletSpawnOffset;
    if (fireMode.CanFire(useType)) {
      projectilePattern.SpawnProjectiles(projectileManager, bulletSpawnPosition, bulletDirection, stats);
    }
  }
}
