using System;
using System.Collections.Generic;
using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class RifleWanderState : IRifleState {
  private RifleSprite rifle;
  private Random random;
  private double wanderTimer;
  private double wanderDuration;

  private double animationTimer;
  private int currentFrameIndex;

  public RifleWanderState(RifleSprite rifle) {
    this.rifle = rifle;
    this.random = new Random();

    this.rifle.CurrentSourceRectangles = new List<Rectangle> {
      new(71, 130, 23, 28),
      new(134, 130, 23, 28),
      new(196, 130, 23, 28),
      new(259, 130, 23, 28),
    };
    this.rifle.CurrentFrame = 0;

    ChangeDirection();
    wanderTimer = 0;
    wanderDuration = 1.0 + (random.NextDouble() * 2.0);
  }

  public void Update(GameTime gameTime) {
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

    animationTimer += dt;
    if (animationTimer >= 0.2) {
      currentFrameIndex++;
      if (currentFrameIndex >= rifle.CurrentSourceRectangles.Count) {
        currentFrameIndex = 0;  
      }

      rifle.CurrentFrame = currentFrameIndex;
      animationTimer = 0;
    }

    rifle.Position += rifle.Velocity * dt;

    if (rifle.Position.X < 0 || rifle.Position.X > 800) {
      rifle.Velocity.X *= -1;
      if (rifle.Velocity.X > 0) {
        rifle.FacingDirection = 1;
      } else if (rifle.Velocity.X < 0) {
        rifle.FacingDirection = -1;
      }
    }
    if (rifle.Position.Y < 0 || rifle.Position.Y > 480) {
      rifle.Velocity.Y *= -1;
    }

    wanderTimer += dt;
    if (wanderTimer >= wanderDuration) {
      rifle.ChangeState(new RifleIdleState(rifle));
    }
  }

  private void ChangeDirection() {
    float speed = 100f;
    float randomX = (float)(random.NextDouble() * 2 - 1);
    float randomY = (float)(random.NextDouble() * 2 - 1);
    Vector2 direction = new(randomX, randomY);

    if (direction != Vector2.Zero) {
      direction.Normalize();
    }

    rifle.Velocity = direction * speed;

    if (rifle.Velocity.X > 0) {
      rifle.FacingDirection = 1;
    }

    if (rifle.Velocity.X < 0) {
      rifle.FacingDirection = -1;
    }
  }
}
