using GameProject.Sprites;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace GameProject.States {
  public class SnakeAttackState : ISnakeState {
    private SnakeSprite snake;
    private double timer;

    private double animationTimer;
    private int currentFrameIndex;

    public SnakeAttackState(SnakeSprite snake) {
      this.snake = snake;

      // LUNGE! Set high velocity in the direction we are facing
      float lungeSpeed = 300f;
      this.snake.Velocity = new Vector2(snake.FacingDirection * lungeSpeed, 0);

      // Set "Attack" animation frames (maybe open mouth?)
      this.snake.CurrentSourceRectangles = new List<Rectangle>
      {
                new Rectangle(10, 115, 12, 14),
                new Rectangle(42, 116, 12, 13),
                new Rectangle(75, 117, 12, 12),
                new Rectangle(107, 118, 12, 11),
                new Rectangle(137, 116, 15, 13),
                new Rectangle(116, 118, 19, 10),
                new Rectangle(195, 120, 20, 8),
                new Rectangle(229, 121, 17, 8),
                new Rectangle(263, 119, 15, 10),
                new Rectangle(297, 117, 14, 12)
            };
      this.snake.CurrentFrame = 0;
    }

   // Inside SnakeAttackState.cs
public void Update(GameTime gameTime) {
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
    timer += dt;
    
    // 1. ADD THIS ANIMATION LOGIC
    // (You might need to define animationTimer and currentFrameIndex at the top of the class first)
    animationTimer += dt;
    if (animationTimer >= 0.05) // Fast animation for attack!
    {
        currentFrameIndex++;
        if (currentFrameIndex >= snake.CurrentSourceRectangles.Count) {
            currentFrameIndex = 0;
        }
        snake.CurrentFrame = currentFrameIndex;
        animationTimer = 0;
    }

    // 2. Existing movement logic
    snake.Position += snake.Velocity * dt;

    if (timer > 0.5) {
        snake.ChangeState(new SnakeIdleState(snake));
    }
}
  }
}
