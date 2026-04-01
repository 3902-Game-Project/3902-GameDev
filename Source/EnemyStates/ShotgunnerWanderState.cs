using System;
using System.Collections.Generic;
using GameProject.Enemies;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class ShotgunnerWanderState : IShotgunnerState {
  private readonly ShotgunnerSprite shotgunner;
  private readonly ILevelManager levelManager;
  private readonly Random random;
  private double wanderTimer;
  private readonly double wanderDuration;

  private double animationTimer;
  private int currentFrameIndex;

  public ShotgunnerWanderState(ShotgunnerSprite shotgunner, ILevelManager levelManager) {
    this.shotgunner = shotgunner;
    this.levelManager = levelManager;
    random = new Random();
    this.shotgunner.CurrentSourceRectangles = [
      new(21, 339, 32, 39),
      new(98, 337, 32, 41),
      new(174, 339, 32, 39),
      new(251, 341, 32, 37),
    ];
    this.shotgunner.CurrentFrame = 0;

    ChangeDirection();
    wanderTimer = 0;
    wanderDuration = 1.0 + (random.NextDouble() * 2.0);
  }

  public void Update(GameTime gameTime) {
    float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

    animationTimer += dt;
    if (animationTimer >= 0.2) {
      currentFrameIndex++;
      if (currentFrameIndex >= shotgunner.CurrentSourceRectangles.Count) {
        currentFrameIndex = 0;
      }

      shotgunner.CurrentFrame = currentFrameIndex;
      animationTimer = 0;
    }

    shotgunner.Position += shotgunner.Velocity * dt;

    if (shotgunner.Position.X < 0 || shotgunner.Position.X > 800) {
      shotgunner.Velocity = new Vector2(-shotgunner.Velocity.X, shotgunner.Velocity.Y);
      if (shotgunner.Velocity.X > 0) shotgunner.FacingDirection = 1;
      else if (shotgunner.Velocity.X < 0) shotgunner.FacingDirection = -1;
    }
    if (shotgunner.Position.Y < 0 || shotgunner.Position.Y > 480) {
      shotgunner.Velocity = new Vector2(shotgunner.Velocity.X, -shotgunner.Velocity.Y);
    }

    wanderTimer += dt;
    if (wanderTimer >= wanderDuration) {
      shotgunner.ChangeState(new ShotgunnerIdleState(shotgunner, levelManager));
    }
  }

  private void ChangeDirection() {
    float speed = 100f;
    float randomX = (float) (random.NextDouble() * 2 - 1);
    float randomY = (float) (random.NextDouble() * 2 - 1);
    Vector2 direction = new(randomX, randomY);

    if (direction != Vector2.Zero) {
      direction.Normalize();
    }

    shotgunner.Velocity = direction * speed;

    if (shotgunner.Velocity.X > 0) {
      shotgunner.FacingDirection = 1;
    }

    if (shotgunner.Velocity.X < 0) {
      shotgunner.FacingDirection = -1;
    }
  }
}
