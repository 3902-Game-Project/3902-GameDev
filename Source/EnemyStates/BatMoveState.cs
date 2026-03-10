using System;
using System.Collections.Generic;
using GameProject.Interfaces;
using GameProject.Enemies;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class BatMoveState : IBatState {
  private BatSprite bat;
  private Random random;
  private double wanderTimer;
  private double wanderDuration;

  private double animationTimer;
  private int currentFrameIndex;

  public BatMoveState(BatSprite bat) {
    this.bat = bat;
    this.random = new Random();
    this.bat.CurrentSourceRectangles = new List<Rectangle> {
      new(38, 97, 17, 21),
      new(70, 102, 17, 15),
      new(102, 102, 15, 21),
    };
    this.bat.CurrentFrame = 0;

    ChangeDirection();
    wanderTimer = 0;
    wanderDuration = 1.0 + (random.NextDouble() * 2.0);
  }

  public void Update(GameTime gameTime) {
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

    animationTimer += dt;
    if (animationTimer >= 0.2) {
      currentFrameIndex++;
      if (currentFrameIndex >= bat.CurrentSourceRectangles.Count) {
        currentFrameIndex = 0;
      }

      bat.CurrentFrame = currentFrameIndex;
      animationTimer = 0;
    }

    bat.Position += bat.Velocity * dt;

    if (bat.Position.X < 0 || bat.Position.X > 800) {
      bat.Velocity = new Vector2(bat.Velocity.X * -1, bat.Velocity.Y);
      if (bat.Velocity.X > 0) {
        bat.FacingDirection = 1;
      } else if (bat.Velocity.X < 0) {
        bat.FacingDirection = -1;
      }
    }
    if (bat.Position.Y < 0 || bat.Position.Y > 480) {
      bat.Velocity = new Vector2(bat.Velocity.X, bat.Velocity.Y * -1);
    }

    wanderTimer += dt;
    if (wanderTimer >= wanderDuration) {
      bat.ChangeState(new BatIdleState(bat));
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

    bat.Velocity = direction * speed;
    if (bat.Velocity.X > 0) {
      bat.FacingDirection = 1;
    }
    if (bat.Velocity.X < 0) {
      bat.FacingDirection = -1;
    }
  }
}
