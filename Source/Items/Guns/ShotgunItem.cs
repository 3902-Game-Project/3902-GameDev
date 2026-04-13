using GameProject.Enums;
using GameProject.Interfaces;
using GameProject.Managers;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

public class ShotgunItem : DefaultGun {

  public ShotgunItem(Texture2D texture, Vector2 position, Game1 game, GunStats stats)
    : base(texture, position, game, stats) {

    this.sourceRectangle = new(0, 10, 27, 9);
    Category = ItemCategory.Primary;

    bulletSpawnOffset = new Vector2(sourceRectangle.Width / 2, -1 * (sourceRectangle.Height / 2 - 3)) * scale;

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

  public void DrawUI(SpriteBatch spriteBatch, Vector2 position, float scale, Color tint) {
    spriteBatch.Draw(
      texture: texture,
      position: position,
      sourceRectangle: sourceRectangle,
      color: tint,
      rotation: 0f,
      origin: Vector2.Zero,
      scale: scale,
      effects: SpriteEffects.None,
      layerDepth: 0f
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
