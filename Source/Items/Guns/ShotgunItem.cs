using GameProject.Enums;
using GameProject.Interfaces;
using GameProject.Managers;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

public class ShotgunItem : IItem {
  public FacingDirection Direction { get; set; } = FacingDirection.Right;
  private Rectangle sourceRectangle = new(0, 10, 27, 9);
  private Vector2 origin;
  private readonly Texture2D texture;
  private readonly float scale = 1f;
  public Vector2 Position { get; set; }

  private readonly Game1 game;
  private readonly IProjectilePattern projectilePattern = new SpreadPattern();
  private Vector2 bulletSpawnOffset;
  private readonly GunStats stats;
  private readonly IFireMode fireMode;
  public ItemCategory Category { get; } = ItemCategory.Primary;

  public ShotgunItem(Texture2D texture, Vector2 position, Game1 game, GunStats stats) {
    this.game = game;
    this.texture = texture;
    Position = position;
    this.stats = stats;
    Position = position;
    bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2, -1 * (sourceRectangle.Height / 2 - 3)) * scale; // Adjust spawn offset based on the shotgun's size and scale
    fireMode = new SemiAutoFire(stats);
  }

  public void Draw(SpriteBatch spriteBatch) {
    origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

    SpriteEffects effects = SpriteEffects.None;
    if (Direction == FacingDirection.Left) {
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

  public void OnPickup(Player player) { }

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
    Vector2 actualOffset = new(offsetX, bulletSpawnOffset.Y);
    Vector2 bulletSpawnPosition = Position + actualOffset;
    if (fireMode.CanFire(useType)) {
      projectilePattern.SpawnProjectiles(game.StateGame.LevelManager.CurrentLevel.ProjectileManager, bulletSpawnPosition, bulletDirection, stats);
      SoundManager.Instance.Play(stats.GunshotID);
    }
  }
}
