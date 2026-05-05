using Microsoft.Xna.Framework;

namespace GameProject.Enemies.SnakeStates;

internal class SnakeAttackState : IEnemyState {
  private readonly Snake snake;
  private double timer = 0.0, animationTimer = 0.0;

  public SnakeAttackState(Snake snake) {
    this.snake = snake;
    int attackDirection = (snake.Direction == FacingDirection.Right) ? 1 : -1;
    this.snake.Velocity = new Vector2(attackDirection * 300f, 0);
    this.snake.CurrentSourceRectangles = [
      new(10, 115, 12, 14),
      new(42, 116, 12, 13),
      new(75, 117, 12, 12),
      new(107, 118, 12, 11),
      new(137, 116, 15, 13),
      new(116, 118, 19, 10),
      new(195, 120, 20, 8),
      new(229, 121, 17, 8),
      new(263, 119, 15, 10),
      new(297, 117, 14, 12),
    ];
    this.snake.CurrentFrame = 0;
  }

  public void Update(double deltaTime) {
    timer += deltaTime;
    animationTimer += deltaTime;

    if (animationTimer >= 0.05) {
      snake.CurrentFrame = (snake.CurrentFrame + 1) % snake.CurrentSourceRectangles.Count;
      animationTimer = 0;
    }

    snake.Position += snake.Velocity * ((float) deltaTime);
    if (timer > 0.5) snake.CurrentState = new SnakeIdleState(snake);
  }
}
