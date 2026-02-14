using GameProject.Sprites;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameProject.States {
  public class SnakeIdleState : ISnakeState {
    private SnakeSprite snake;
    private double timer;
    private System.Random random;

    public SnakeIdleState(SnakeSprite snake) {
      this.snake = snake;
      this.random = new System.Random();

      this.snake.Velocity = Vector2.Zero;

      this.snake.CurrentSourceRectangles = new List<Rectangle>
      {
                new Rectangle(11, 20, 10, 12),
                new Rectangle(43, 21, 10, 11),
                new Rectangle(75, 22, 10, 10),
                new Rectangle(106, 22, 11, 10),
                new Rectangle(138, 22, 11, 10),
                new Rectangle(170, 22, 11, 10),
                new Rectangle(203, 22, 10, 10),
                new Rectangle(235, 21, 10, 11),
                new Rectangle(267, 20, 10, 12),
                new Rectangle(299, 20, 10, 12)
            };
      this.snake.CurrentFrame = 0;
    }

    public void Update(GameTime gameTime) {
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
}
