using GameProject.Enemies.RiflemanStates;
using GameProject.Enemies.States;
using GameProject.Factories;
using GameProject.Managers;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

internal class Rifleman : ABaseEnemy {
  public ILevelManager LevelManager { get; }

  public Rifleman(Texture2D texture, Vector2 position, ILevelManager levelManager) : base(texture, position, 48f, 96f) {
    LevelManager = levelManager;
    DrawScale = 2f;
    FlipOnRightDir = false;
    CurrentState = new RifleWanderState(this);
  }

  public void FireProjectile(int damage) {
    Vector2 spawnPosition = Position + new Vector2(FacingDirection * 15f, -33f);
    IProjectile bullet = ProjectileFactory.Instance.CreateBullet(spawnPosition, new Vector2(FacingDirection, 0f), 300f, 2f, damage);
    if (bullet is BulletDefault b) b.IsPlayerShot = false;
    LevelManager.CurrentLevel.ProjectileManager.Add(bullet);
  }

  protected override void DropLoot() {
    LevelManager.CurrentLevel.AddPickup(WorldPickupFactory.Instance.CreateAmmo(Position, Items.AmmoType.Heavy, 5));
  }

  protected override void TransitionToDeathState() {
    CurrentState = new GenericDeathState(this, [new(11, 9, 21, 28), new(73, 11, 23, 26), new(135, 16, 33, 21), new(198, 20, 40, 17), new(260, 22, 40, 15), new(323, 23, 39, 14), new(385, 25, 40, 12)]);
  }
}
