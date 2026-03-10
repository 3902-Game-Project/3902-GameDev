using System.Collections.Generic;
using GameProject.Factories;
using GameProject.Interfaces;
using GameProject.Source.Enemies;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class ShotgunnerAttackState : IShotgunnerState {
  private ShotgunnerSprite shotgunner;
  private double stateTimer;
  private double animationTimer;
  private double timePerFrame = 0.15;

  private bool hasFired = false;

  public ShotgunnerAttackState(ShotgunnerSprite shotgunner) {
    this.shotgunner = shotgunner;

    this.shotgunner.Velocity = Vector2.Zero;

    this.shotgunner.CurrentSourceRectangles = new List<Rectangle> {
      new(23, 418, 35, 37),
      new(100, 415, 32, 40),
      new(174, 415, 39, 40),
    };
    this.shotgunner.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) {
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
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
      shotgunner.ChangeState(new ShotgunnerIdleState(shotgunner));
    }
  }

  private void FireBullets() {
    Vector2 spawnOffset = new Vector2(shotgunner.FacingDirection * 15f, -30f);
    Vector2 spawnPosition = shotgunner.Position + spawnOffset;
    float spreadY = 0.25f;

    Vector2 dirStraight = new Vector2(shotgunner.FacingDirection, 0f);
    dirStraight.Normalize();

    Vector2 dirUp = new Vector2(shotgunner.FacingDirection, -spreadY);
    dirUp.Normalize();

    Vector2 dirDown = new Vector2(shotgunner.FacingDirection, spreadY);
    dirDown.Normalize();
    float bulletSpeed = 400f;
    float bulletLifetime = 0.6f;

    IProjectile bullet1 = ProjectileFactory.Instance.CreateBullet(spawnPosition, dirStraight, bulletSpeed, bulletLifetime);
    IProjectile bullet2 = ProjectileFactory.Instance.CreateBullet(spawnPosition, dirUp, bulletSpeed, bulletLifetime);
    IProjectile bullet3 = ProjectileFactory.Instance.CreateBullet(spawnPosition, dirDown, bulletSpeed, bulletLifetime);

    // Add them all to the manager!
    if (shotgunner.ProjectileManager != null) {
      shotgunner.ProjectileManager.AddProjectile(bullet1);
      shotgunner.ProjectileManager.AddProjectile(bullet2);
      shotgunner.ProjectileManager.AddProjectile(bullet3);
    }
  }
}
