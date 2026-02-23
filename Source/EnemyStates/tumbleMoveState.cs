using System;
using System.Collections.Generic;
using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class TumbleMoveState : ITumbleState {
  private TumbleSprite tumbleweed;
  private Random random;
  private double wanderTimer;
  private double wanderDuration;

  private double animationTimer;
  private int currentFrameIndex;

  public TumbleMoveState(TumbleSprite tumbleweed) {
    this.tumbleweed = tumbleweed;
    this.random = new Random();

    this.tumbleweed.CurrentSourceRectangles = new List<Rectangle> {
      new(36, 41, 108, 106),
      new(202, 42, 109, 105),
      new(366, 42, 109, 105),
      new(533, 41, 107, 106)
    };
    this.tumbleweed.CurrentFrame = 0;

    ChangeDirection();
    wanderTimer = 0;
    wanderDuration = 1.0 + (random.NextDouble() * 2.0);
  }

  public void Update(GameTime gameTime) {
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

    animationTimer += dt;
    if (animationTimer >= 0.2) {
      currentFrameIndex++;
      if (currentFrameIndex >= tumbleweed.CurrentSourceRectangles.Count) {
        currentFrameIndex = 0;
      }

      tumbleweed.CurrentFrame = currentFrameIndex;
      animationTimer = 0;
    }

    tumbleweed.Position += tumbleweed.Velocity * dt;

    if (tumbleweed.Position.X < 0 || tumbleweed.Position.X > 800) {
      tumbleweed.Velocity.X *= -1;
      if (tumbleweed.Velocity.X > 0) {
        tumbleweed.FacingDirection = 1;
      } else if (tumbleweed.Velocity.X < 0) {
        tumbleweed.FacingDirection = -1;
      }
    }
    if (tumbleweed.Position.Y < 0 || tumbleweed.Position.Y > 480) {
      tumbleweed.Velocity.Y *= -1;
    }

    wanderTimer += dt;
    if (wanderTimer >= wanderDuration) {
      tumbleweed.ChangeState(new TumbleIdleState(tumbleweed));
    }
  }

  private void ChangeDirection() {
    float speed = 75f;
    float randomX = (float)(random.NextDouble() * 2 - 1);

    Vector2 direction = new(randomX, 0f);

    if (direction != Vector2.Zero) {
      direction.Normalize();
    }

    tumbleweed.Velocity = direction * speed;

    if (tumbleweed.Velocity.X > 0) {
      tumbleweed.FacingDirection = 1;
    }
    if (tumbleweed.Velocity.X < 0) {
      tumbleweed.FacingDirection = -1;
    }
  }
}
