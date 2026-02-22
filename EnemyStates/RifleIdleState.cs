using System.Collections.Generic;
using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class RifleIdleState : IRifleState {
  private RifleSprite rifle;
  private double timer;
  private double animationTimer;
  private System.Random random;

  public RifleIdleState(RifleSprite Rifle) {
    this.rifle = rifle;
    this.random = new System.Random();

    this.rifle.Velocity = Vector2.Zero;
    this.rifle.CurrentSourceRectangles = new List<Rectangle> {
      new(21, 339, 32, 39),
      new(98, 337, 32, 41),
      new(174, 339, 32, 39),
      new(251, 341, 32, 37),
    };
    this.rifle.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) {
    animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
    if (animationTimer > 0.2) {
      rifle.CurrentFrame++;
      if (rifle.CurrentFrame >= rifle.CurrentSourceRectangles.Count) {
        rifle.CurrentFrame = 0;
      }
      animationTimer = 0;
    }
    timer += gameTime.ElapsedGameTime.TotalSeconds;

    if (timer > 1.0) {
      int choice = random.Next(0, 2);
      if (choice == 0) {
        rifle.ChangeState(new RifleAttackState(rifle));
      } else {
        rifle.ChangeState(new RifleWanderState(rifle));
      }
    }
  }
}
