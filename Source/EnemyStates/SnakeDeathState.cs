using System.Collections.Generic;
using GameProject.Enemies;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class SnakeDeathState : ISnakeState {
  private readonly SnakeSprite snake;

  private double animationTimer;
  private readonly double timePerFrame = 0.15;

  private double deadHoldTimer;
  private readonly double timeToHoldLastFrame = 1.5;

  private bool isAnimationFinished = false;

  public SnakeDeathState(SnakeSprite snake) {
    this.snake = snake;
    this.snake.Velocity = Vector2.Zero;

    this.snake.CurrentSourceRectangles = [
      new(76, 143, 8, 17),
      new(108, 143, 8, 17),
      new(140, 143, 8, 17),
      new(43, 146, 10, 14),
      new(171, 146, 9, 14),
      new(10, 147, 12, 13),
      new(203, 147, 11, 13),
      new(235, 151, 12, 9),
      new(267, 153, 13, 7),
      new(299, 154, 15, 6)
    ];
    this.snake.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) {
    float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

    if (!isAnimationFinished) {
      animationTimer += dt;
      if (animationTimer >= timePerFrame) {
        if (snake.CurrentFrame < snake.CurrentSourceRectangles.Count - 1) {
          snake.CurrentFrame++;
          animationTimer = 0;
        } else {
          isAnimationFinished = true;
        }
      }
    } else {
      deadHoldTimer += dt;
      if (deadHoldTimer >= timeToHoldLastFrame) {
        snake.CurrentSourceRectangles.Clear();
      }
    }
  }
}
