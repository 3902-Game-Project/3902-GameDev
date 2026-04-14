using GameProject.Factories;
using GameProject.Managers;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.ShotgunnerStates;

internal class ShotgunnerAttackState : IShotgunnerState {
  private readonly ShotgunnerSprite shotgunner;
  private readonly ILevelManager levelManager;
  private double stateTimer;
  private double animationTimer;
  private readonly double timePerFrame = 0.15;

  private bool hasFired = false;

  public ShotgunnerAttackState(ShotgunnerSprite shotgunner, ILevelManager levelManager) {
    this.shotgunner = shotgunner;
    this.levelManager = levelManager;

    this.shotgunner.Velocity = Vector2.Zero;

    this.shotgunner.CurrentSourceRectangles = [
      new(23, 418, 35, 37),
      new(100, 415, 32, 40),
      new(174, 415, 39, 40),
    ];
    this.shotgunner.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) {
    float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
    animationTimer += dt;
    if (animationTimer >= timePerFrame) {
      if (shotgunner.CurrentFrame < shotgunner.CurrentSourceRectangles.Count - 1) {
        shotgunner.CurrentFrame++;
        animationTimer = 0;
      }
    }

    if (shotgunner.CurrentFrame == shotgunner.CurrentSourceRectangles.Count - 1 && !hasFired) {
      FireBullets();
      hasFired = true;
    }

    // 3. State Transition
    stateTimer += dt;
    if (stateTimer > 1.0) {
      shotgunner.ChangeState(new ShotgunnerIdleState(shotgunner, levelManager));
    }
  }

  private void FireBullets() {
    Vector2 spawnOffset = new(shotgunner.FacingDirection * 15f, -30f);
    Vector2 spawnPosition = shotgunner.Position + spawnOffset;
    float spreadY = 0.25f;

    Vector2 dirStraight = new(shotgunner.FacingDirection, 0f);
    dirStraight.Normalize();

    Vector2 dirUp = new(shotgunner.FacingDirection, -spreadY);
    dirUp.Normalize();

    Vector2 dirDown = new(shotgunner.FacingDirection, spreadY);
    dirDown.Normalize();
    float bulletSpeed = 400f;
    float bulletLifetime = 0.6f;

    IProjectile bullet1 = ProjectileFactory.Instance.CreateBullet(spawnPosition, dirStraight, bulletSpeed, bulletLifetime);
    IProjectile bullet2 = ProjectileFactory.Instance.CreateBullet(spawnPosition, dirUp, bulletSpeed, bulletLifetime);
    IProjectile bullet3 = ProjectileFactory.Instance.CreateBullet(spawnPosition, dirDown, bulletSpeed, bulletLifetime);

    if (bullet1 is BulletDefault b1) {
      b1.IsPlayerShot = false;
    }
    if (bullet2 is BulletDefault b2) {
      b2.IsPlayerShot = false;
    }
    if (bullet3 is BulletDefault b3) {
      b3.IsPlayerShot = false;
    }

    // Add them all to the manager!
    levelManager.CurrentLevel.ProjectileManager.Add(bullet1);
    levelManager.CurrentLevel.ProjectileManager.Add(bullet2);
    levelManager.CurrentLevel.ProjectileManager.Add(bullet3);
  }
}
