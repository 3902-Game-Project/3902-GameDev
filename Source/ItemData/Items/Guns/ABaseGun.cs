using System;
using GameProject.Controllers;
using GameProject.FireModes;
using GameProject.Misc;
using GameProject.PlayerSpace;
using GameProject.ProjectilePatterns;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

internal abstract class ABaseGun(Texture2D texture, Vector2 startPosition, Player player, ProjectileManagerGetter GetProjectileManager, GunStats stats) : IItem {
  private protected static readonly float SCALE = 1.0f;

  private protected readonly Texture2D texture = texture;
  private protected readonly GunStats stats = stats;

  private protected IProjectilePattern projectilePattern = new SingleShotPattern();
  private protected IFireMode fireMode;
  private protected Rectangle sourceRectangle;
  private protected Vector2 origin;
  private protected Vector2 bulletSpawnOffset;

  internal FacingDirection Direction { get; set; } = FacingDirection.Right;
  internal ItemCategory Category { get; private protected set; }
  internal Vector2 Position { get; set; } = startPosition;

  // Ammo and reload state
  internal double EquipTimer { get; private set; } = 0.0;
  internal double ReloadTimer { get; private set; } = 0.0;
  internal bool IsReloading { get; private set; } = false;

  internal GunStats Stats => stats;

  internal virtual void OnEquip() {
    EquipTimer = stats.EquipTime; // Prevent double-pumping
    IsReloading = false;
    fireMode?.OnEquip();
  }

  internal virtual void OnUnequip() {
    IsReloading = false;
    fireMode?.OnUnequip();
  }

  internal void StartReload() {
    if (IsReloading) {
      return;
    }
    if (stats.CurrentAmmo < stats.MaxAmmo && player.Inventory.Ammo[stats.AmmoType] > 0) {
      IsReloading = true;
      ReloadTimer = stats.ReloadTime;
    }
  }

  internal virtual void Update(double deltaTime) {
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

  internal virtual void Draw(SpriteBatch spriteBatch) {
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

  internal void DrawUI(SpriteBatch spriteBatch, Vector2 position, float scale, Color tint) {
    spriteBatch.Draw(texture, position, sourceRectangle, tint, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
  }

  internal virtual void OnPickup(Player player) { }

  internal virtual void Use(UseType useType) {
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
    projectilePattern.SpawnProjectiles(GetProjectileManager(), bulletSpawnPosition, bulletDirection, stats);
    SoundManager.Instance.Play(stats.GunshotID);
  }
}
