using System.Collections.Generic;
using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class SnakeAttackState : ISnakeState {
  private SnakeSprite snake;
  private double timer;
  private double animationTimer;

  public SnakeAttackState(SnakeSprite snake) {
    this.snake = snake;
    float lungeSpeed = 300f;

    // Lunge in the direction we are currently looking
    this.snake.Velocity = new Vector2(snake.FacingDirection * lungeSpeed, 0);

    this.snake.CurrentSourceRectangles = new List<Rectangle> {
      new(10, 115, 12, 14),
      new(42, 116, 12, 13),
      new(75, 117, 12, 12),
      new(107, 118, 12, 11),
      new(137, 116, 15, 13),
      new(116, 118, 19, 10),
      new(195, 120, 20, 8),
      new(229, 121, 17, 8),
      new(263, 119, 15, 10),
      new(297, 117, 14, 12)
    };
    this.snake.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) {
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
    timer += dt;

    animationTimer += dt;
    if (animationTimer >= 0.05) {
      snake.CurrentFrame++;
      if (snake.CurrentFrame >= snake.CurrentSourceRectangles.Count) {
        snake.CurrentFrame = 0;
      }

      animationTimer = 0;
    }

    // 2. Movement
    snake.Position += snake.Velocity * dt;

    // 3. Return to Idle
    if (timer > 0.5) {
      snake.ChangeState(new SnakeIdleState(snake));
    }
  }
}
