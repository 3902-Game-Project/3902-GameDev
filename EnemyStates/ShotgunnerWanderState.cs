using GameProject.Sprites;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GameProject.States {
  public class ShotgunnerWanderState : IShotgunnerState {
    private ShotgunnerSprite shotgunner;
    private Random random;
    private double wanderTimer;
    private double wanderDuration;

    private double animationTimer;
    private int currentFrameIndex;

    public ShotgunnerWanderState(ShotgunnerSprite shotgunner) {
      this.shotgunner = shotgunner;
      this.random = new Random();
      this.shotgunner.CurrentSourceRectangles = new List<Rectangle>
      {
          new Rectangle(9, 142, 13, 16),
          new Rectangle(41, 141, 13, 17),
          new Rectangle(73, 142, 13, 16),
          new Rectangle(105, 143, 13, 15),
      };
      this.shotgunner.CurrentFrame = 0;

      ChangeDirection();
      wanderTimer = 0;
      wanderDuration = 1.0 + (random.NextDouble() * 2.0);
    }

    public void Update(GameTime gameTime) {
      float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

      animationTimer += dt;
      if (animationTimer >= 0.2) {
        currentFrameIndex++;
        if (currentFrameIndex >= shotgunner.CurrentSourceRectangles.Count) currentFrameIndex = 0;
        shotgunner.CurrentFrame = currentFrameIndex;
        animationTimer = 0;
      }

      shotgunner.Position += shotgunner.Velocity * dt;

      if (shotgunner.Position.X < 0 || shotgunner.Position.X > 800) {
        shotgunner.Velocity.X *= -1;
        if (shotgunner.Velocity.X > 0) shotgunner.FacingDirection = 1;
        else if (shotgunner.Velocity.X < 0) shotgunner.FacingDirection = -1;
      }
      if (shotgunner.Position.Y < 0 || shotgunner.Position.Y > 480) {
        shotgunner.Velocity.Y *= -1;
      }

      wanderTimer += dt;
      if (wanderTimer >= wanderDuration) {
        shotgunner.ChangeState(new ShotgunnerIdleState(shotgunner));
      }
    }

    private void ChangeDirection() {
      float speed = 100f;
      float randomX = (float)(random.NextDouble() * 2 - 1);
      float randomY = (float)(random.NextDouble() * 2 - 1);
      Vector2 direction = new Vector2(randomX, randomY);

      if (direction != Vector2.Zero) direction.Normalize();

      shotgunner.Velocity = direction * speed;
      if (shotgunner.Velocity.X > 0) shotgunner.FacingDirection = 1;
      if (shotgunner.Velocity.X < 0) shotgunner.FacingDirection = -1;
    }
  }
}
