using System;
using GameProject.Controllers;
using GameProject.FireModes;
using GameProject.Level;
using GameProject.Misc;
using GameProject.PlayerSpace;
using GameProject.ProjectilePatterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal abstract class ABaseGun(Texture2D texture, Vector2 startPosition, Player player, ILevelManager levelManager, GunStats stats) : IItem {
  protected static readonly float SCALE = 1.0f;

  protected readonly Texture2D texture = texture;
  protected readonly GunStats stats = stats;

  protected IProjectilePattern projectilePattern = new SingleShotPattern();
  protected IFireMode fireMode;
  protected Rectangle sourceRectangle;
  protected Vector2 origin;
  protected Vector2 bulletSpawnOffset;

  public FacingDirection Direction { get; set; } = FacingDirection.Right;
  public ItemCategory Category { get; protected set; }
  public Vector2 Position { get; set; } = startPosition;

  // Ammo and reload state
  public double EquipTimer { get; private set; } = 0.0;
  public double ReloadTimer { get; private set; } = 0.0;
  public bool IsReloading { get; private set; } = false;

  public GunStats Stats => stats;

  public virtual void OnEquip() {
    EquipTimer = stats.EquipTime; // Prevent double-pumping
    IsReloading = false;
    fireMode?.OnEquip();
  }

  public virtual void OnUnequip() {
    IsReloading = false;
    fireMode?.OnUnequip();
  }

  public void StartReload() {
    if (IsReloading) {
      return;
    }
    if (stats.CurrentAmmo < stats.MaxAmmo && player.Inventory.Ammo[stats.AmmoType] > 0) {
      IsReloading = true;
      ReloadTimer = stats.ReloadTime;
    }
  }

  public virtual void Update(double deltaTime) {
    if (EquipTimer > 0) {
      EquipTimer -= deltaTime;
    } else if (IsReloading) {
      ReloadTimer -= deltaTime;
      if (ReloadTimer <= 0) {
        int ammoNeeded = stats.MaxAmmo - stats.CurrentAmmo;
        int ammoAvailable = player.Inventory.Ammo[stats.AmmoType];

        if (ammoAvailable > 0 && ammoNeeded > 0) {
          int toLoad = stats.ReloadsOneByOne ? 1 : Math.Min(ammoNeeded, ammoAvailable);
          stats.CurrentAmmo += toLoad;
          player.Inventory.Ammo[stats.AmmoType] -= toLoad;

          // Loop the reload if more still needed
          if (stats.CurrentAmmo < stats.MaxAmmo && player.Inventory.Ammo[stats.AmmoType] > 0) {
            ReloadTimer = stats.ReloadTime;
          } else {
            IsReloading = false;
          }
        } else {
          IsReloading = false;
        }
      }
    }

    fireMode?.Update(deltaTime);
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

    spriteBatch.Draw(texture, Position, sourceRectangle, Color.White, rotation, origin, SCALE, effects, 0f);
  }

  public void DrawUI(SpriteBatch spriteBatch, Vector2 position, float scale, Color tint) {
    spriteBatch.Draw(texture, position, sourceRectangle, tint, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
  }

  public virtual void OnPickup(Player player) { }

  public virtual void Use(UseType useType) {
    // Check for Reload Interruption
    if (IsReloading && stats.CurrentAmmo > 0) {
      IsReloading = false;
      //EquipTimer = stats.EquipTime; // Applying the interrupt penalty
      //return;
    }

    // Prevent firing if equipping or empty
    if (EquipTimer > 0 || stats.CurrentAmmo <= 0) return;

    if (fireMode == null || !fireMode.CanFire(useType)) return;
    stats.CurrentAmmo--;

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
    projectilePattern.SpawnProjectiles(levelManager.CurrentLevel.ProjectileManager, bulletSpawnPosition, bulletDirection, stats);
    SoundManager.Instance.Play(stats.GunshotID);
  }
}
