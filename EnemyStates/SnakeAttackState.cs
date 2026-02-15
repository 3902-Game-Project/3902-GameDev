using GameProject.Sprites;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameProject.States {
  public class SnakeAttackState : ISnakeState {
    private SnakeSprite snake;
    private double timer;
    private double animationTimer;

    public SnakeAttackState(SnakeSprite snake) {
      this.snake = snake;
      float lungeSpeed = 300f;

      // Lunge in the direction we are currently looking
      this.snake.Velocity = new Vector2(snake.FacingDirection * lungeSpeed, 0);

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

    public void Update(GameTime gameTime) {
      float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
      timer += dt;

      animationTimer += dt;
      if (animationTimer >= 0.05)
      {
        snake.CurrentFrame++;
        if (snake.CurrentFrame >= snake.CurrentSourceRectangles.Count)
          snake.CurrentFrame = 0;
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
}
