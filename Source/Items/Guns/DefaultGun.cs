using GameProject.Enums;
using GameProject.Interfaces;
using GameProject.Managers;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

public abstract class DefaultGun : IItem {
  public FacingDirection Direction { get; set; } = FacingDirection.Right;
  public ItemCategory Category { get; protected set; }
  public Vector2 Position { get; set; }

  protected readonly Texture2D texture;
  protected readonly float scale = 1f;
  protected readonly Game1 game;
  protected readonly GunStats stats;

  protected IProjectilePattern projectilePattern;
  protected IFireMode fireMode;
  protected Rectangle sourceRectangle;
  protected Vector2 origin;
  protected Vector2 bulletSpawnOffset;

  public GunStats PublicStats => stats;

  protected DefaultGun(Texture2D texture, Vector2 startPosition, Game1 game, GunStats stats) {
    this.game = game;
    this.texture = texture;
    this.stats = stats;
    Position = startPosition;

    projectilePattern = new SingleShotPattern();
  }

  public virtual void OnEquip() {
    fireMode?.OnEquip();
  }
  public virtual void OnUnequip() {
    fireMode?.OnUnequip();
  }

  public virtual void Draw(SpriteBatch spriteBatch) {
    origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

    SpriteEffects effects = SpriteEffects.None;
    float rotation = 0f;
    if (Direction == FacingDirection.Left) {
      effects = SpriteEffects.FlipHorizontally;
    } else if (Direction == FacingDirection.Up) {
      rotation = -MathHelper.PiOver2;
    } else if (Direction == FacingDirection.Down) {
      rotation = MathHelper.PiOver2;
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

  public void DrawUI(SpriteBatch spriteBatch, Vector2 position, float scale, Color tint) {
    spriteBatch.Draw(
      texture: texture,
      position: position, // This was the missing line!
      sourceRectangle: sourceRectangle,
      color: tint,
      rotation: 0f,
      origin: Vector2.Zero,
      scale: scale,
      effects: SpriteEffects.None,
      layerDepth: 0f
    );
  }

  public virtual void Update(GameTime gameTime) {
    fireMode?.Update(gameTime);
  }

  public virtual void OnPickup(Player player) { }

  public virtual void Use(UseType useType) {
    if (fireMode == null || !fireMode.CanFire(useType)) return;

    Vector2 bulletDirection;
    Vector2 actualOffset;
    if (Direction == FacingDirection.Left) {
      bulletDirection = new Vector2(-1, 0);
      actualOffset = new Vector2(-bulletSpawnOffset.X, bulletSpawnOffset.Y);
    } else if (Direction == FacingDirection.Right) {
      bulletDirection = new Vector2(1, 0);
      actualOffset = new Vector2(bulletSpawnOffset.X, bulletSpawnOffset.Y);
    } else if (Direction == FacingDirection.Up) {
      bulletDirection = new Vector2(0, -1);
      actualOffset = new Vector2(bulletSpawnOffset.Y, -bulletSpawnOffset.X);
    } else { // Down
      bulletDirection = new Vector2(0, 1);
      actualOffset = new Vector2(-bulletSpawnOffset.Y, bulletSpawnOffset.X);
    }

    Vector2 bulletSpawnPosition = Position + actualOffset;

    projectilePattern.SpawnProjectiles(game.StateGame.LevelManager.CurrentLevel.ProjectileManager, bulletSpawnPosition, bulletDirection, stats);
    SoundManager.Instance.Play(stats.GunshotID);
  }
}
