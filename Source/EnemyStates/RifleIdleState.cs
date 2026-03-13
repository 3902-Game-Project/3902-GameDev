using System.Collections.Generic;
using GameProject.Enemies;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class RifleIdleState : IRiflemanState {
  private RiflemanSprite rifle;
  private Game1 game;
  private double timer;
  private double animationTimer;
  private System.Random random;

  public RifleIdleState(RiflemanSprite rifle, Game1 game) {
    this.rifle = rifle;
    this.game = game;
    this.random = new System.Random();

    this.rifle.Velocity = Vector2.Zero;

    this.rifle.CurrentSourceRectangles = new List<Rectangle> {
      new(71, 130, 23, 28),
      new(134, 130, 23, 28),
      new(196, 130, 23, 28),
      new(259, 130, 23, 28),
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
        rifle.ChangeState(new RifleAttackState(rifle, game));
      } else {
        rifle.ChangeState(new RifleWanderState(rifle, game));
      }
    }
  }
}
