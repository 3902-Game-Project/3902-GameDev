using System.Collections.Generic;
using GameProject.Enemies;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class SnakeIdleState : ISnakeState {
  private SnakeSprite snake;
  private double timer;
  private double animationTimer;
  private System.Random random;

  public SnakeIdleState(SnakeSprite snake) {
    this.snake = snake;
    this.random = new System.Random();

    this.snake.Velocity = Vector2.Zero;
    this.snake.CurrentSourceRectangles = new List<Rectangle> {
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
    };
    this.snake.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) {
    animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
    if (animationTimer > 0.1) {
      snake.CurrentFrame++;
      if (snake.CurrentFrame >= snake.CurrentSourceRectangles.Count) {
        snake.CurrentFrame = 0;
      }
      animationTimer = 0;
    }
    timer += gameTime.ElapsedGameTime.TotalSeconds;

    if (timer > 1.0) {
      int choice = random.Next(0, 2);
      if (choice == 0) {
        snake.ChangeState(new SnakeAttackState(snake));
      } else {
        snake.ChangeState(new SnakeWanderState(snake));
      }
    }
  }
}
