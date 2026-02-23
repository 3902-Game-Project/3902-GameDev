using System.Collections.Generic;
using GameProject.Interfaces;
using GameProject.Sprites;
using GameProject.Factories;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class RifleAttackState : IRifleState {
  private RifleSprite rifle;
  private double stateTimer;
  private double animationTimer;
  private double timePerFrame = 0.15;

  private bool hasFired = false;

  public RifleAttackState(RifleSprite rifle) {
    this.rifle = rifle;

    this.rifle.Velocity = Vector2.Zero;

    this.rifle.CurrentSourceRectangles = new List<Rectangle> {
      new(198, 91, 21, 27),
      new(260, 91, 22, 27),
      new(323, 89, 23, 29),
    };
    this.rifle.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) {
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

    animationTimer += dt;
    if (animationTimer >= timePerFrame) {
      if (rifle.CurrentFrame < rifle.CurrentSourceRectangles.Count - 1) {
        rifle.CurrentFrame++;
        animationTimer = 0;
      }
    }

    if (rifle.CurrentFrame == rifle.CurrentSourceRectangles.Count - 1 && !hasFired) {
      FireBullet();
      hasFired = true;
    }

    stateTimer += dt;
    if (stateTimer > 1.0) {
      rifle.ChangeState(new RifleIdleState(rifle));
    }
  }

  private void FireBullet() {
    Vector2 bulletDirection = new Vector2(rifle.FacingDirection, 0f);

    // Calculate a spawn point so it comes out of the gun barrel, not his feet.
    Vector2 spawnOffset = new Vector2(rifle.FacingDirection * 15f, -33f);
    Vector2 spawnPosition = rifle.Position + spawnOffset;

    // Create the bullet (Velocity: 300f, Lifetime: 2 seconds)
    IProjectile bullet = ProjectileFactory.Instance.CreateBullet(spawnPosition, bulletDirection, 300f, 2f);

    if (rifle.ProjectileManager != null) {
      rifle.ProjectileManager.AddProjectile(bullet);
    }
  }
}
