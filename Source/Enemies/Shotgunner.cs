using GameProject.Enemies.ShotgunnerStates;
using GameProject.Enemies.States;
using GameProject.Factories;
using GameProject.Managers;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

internal class Shotgunner : ABaseEnemy {
  public ILevelManager LevelManager { get; }

  public Shotgunner(Texture2D texture, Vector2 position, ILevelManager levelManager) : base(texture, position, 48f, 96f) {
    LevelManager = levelManager;
    DrawScale = 1.6f;
    FlipOnRightDir = false;
    CurrentState = new ShotgunnerWanderState(this);
  }

  public void FireSpread(int damage) {
    Vector2 bulletDirection = Vector2.Zero;
    switch (Direction) {
      case FacingDirection.Left:
        bulletDirection = new Vector2(-1f, 0f);
        break;

      case FacingDirection.Right:
        bulletDirection = new Vector2(1f, 0f);
        break;
    
      case FacingDirection.Up:
        bulletDirection = new Vector2(0f, -1f);
        break;
    
      case FacingDirection.Down:
        bulletDirection = new Vector2(0f, 1f);
        break;

      default:
        break;
    
    }
    Vector2 spawnPosition = Position + new Vector2(0f, -30f) + (bulletDirection * 15f);
    Vector2 spreadPerpendicular = new(-bulletDirection.Y, bulletDirection.X);

    foreach (float spreadAmount in new[] { 0f, -0.25f, 0.25f }) {
      Vector2 dir = bulletDirection + (spreadPerpendicular * spreadAmount);
      dir.Normalize();
      IProjectile bullet = ProjectileFactory.Instance.CreateBullet(spawnPosition, dir, 400f, 0.6f, damage);
      if (bullet is BulletDefault b) b.IsPlayerShot = false;
      LevelManager.CurrentLevel.ProjectileManager.Add(bullet);
    }
  }

  protected override void DropLoot() {
    LevelManager.CurrentLevel.AddPickup(WorldPickupFactory.Instance.CreateAmmo(Position, Items.AmmoType.Shells, 5));
  }

  protected override void TransitionToDeathState() {
    CurrentState = new GenericDeathState(this, [new(14, 568, 39, 40), new(100, 573, 37, 35), new(174, 576, 42, 32), new(246, 585, 51, 23)]);
  }
}
