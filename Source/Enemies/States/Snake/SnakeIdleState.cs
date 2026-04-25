using System;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.SnakeStates;

internal class SnakeIdleState : IEnemyState {
  private readonly Snake snake;
  private double timer = 0.0, animationTimer = 0.0;
  private readonly Random random = new();

  public SnakeIdleState(Snake snake) {
    this.snake = snake;
    this.snake.Velocity = Vector2.Zero;
    this.snake.CurrentSourceRectangles = [
      new(11, 20, 10, 12),
      new(43, 21, 10, 11),
      new(75, 22, 10, 10),
      new(106, 22, 11, 10),
      new(138, 22, 11, 10),
      new(170, 22, 11, 10),
      new(203, 22, 10, 10),
      new(235, 21, 10, 11),
      new(267, 20, 10, 12),
      new(299, 20, 10, 12),
    ];
    this.snake.CurrentFrame = 0;
  }

  public void Update(double deltaTime) {
    animationTimer += deltaTime;
    if (animationTimer > 0.1) {
      snake.CurrentFrame = (snake.CurrentFrame + 1) % snake.CurrentSourceRectangles.Count;
      animationTimer = 0;
    }

    timer += deltaTime;
    if (timer > 1.0) snake.CurrentState = random.Next(0, 2) == 0 ? new SnakeAttackState(snake) : new SnakeWanderState(snake);
  }
}
