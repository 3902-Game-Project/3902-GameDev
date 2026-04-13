using System;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.SnakeStates;

internal class SnakeWanderState : ISnakeState {
  private readonly SnakeSprite snake;
  private readonly Random random;
  private double wanderTimer;
  private readonly double wanderDuration;

  private double animationTimer;
  private int currentFrameIndex;

  public SnakeWanderState(SnakeSprite snake) {
    this.snake = snake;
    random = new Random();
    this.snake.CurrentSourceRectangles = [
      new(10, 84, 13, 13),
      new(43, 84, 13, 13),
      new(75, 84, 13, 13),
      new(105, 84, 14, 13),
      new(136, 84, 14, 13),
      new(166, 85, 15, 12),
      new(198, 85, 16, 12),
      new(232, 85, 15, 12),
      new(266, 85, 12, 12),
      new(298, 85, 12, 12)
    ];
    this.snake.CurrentFrame = 0;

    ChangeDirection();
    wanderTimer = 0;
    wanderDuration = 1.0 + (random.NextDouble() * 2.0);
  }

  public void Update(GameTime gameTime) {
    float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

    animationTimer += dt;
    if (animationTimer >= 0.2) {
      currentFrameIndex++;
      if (currentFrameIndex >= snake.CurrentSourceRectangles.Count) {
        currentFrameIndex = 0;
      }

      snake.CurrentFrame = currentFrameIndex;
      animationTimer = 0;
    }

    snake.Position += snake.Velocity * dt;

    if (snake.Position.X < 0 || snake.Position.X > 800) {
      snake.Velocity = new Vector2(-snake.Velocity.X, snake.Velocity.Y);
      if (snake.Velocity.X > 0) snake.FacingDirection = 1;
      else if (snake.Velocity.X < 0) snake.FacingDirection = -1;
    }
    if (snake.Position.Y < 0 || snake.Position.Y > 480) {
      snake.Velocity = new Vector2(snake.Velocity.X, -snake.Velocity.Y);
    }

    wanderTimer += dt;
    if (wanderTimer >= wanderDuration) {
      snake.ChangeState(new SnakeIdleState(snake));
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

    snake.Velocity = direction * speed;

    if (snake.Velocity.X > 0) {
      snake.FacingDirection = 1;
    }

    if (snake.Velocity.X < 0) {
      snake.FacingDirection = -1;
    }
  }
}
