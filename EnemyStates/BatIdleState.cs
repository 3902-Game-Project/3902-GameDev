using System.Collections.Generic;
using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class BatIdleState : IBatState {
  private BatSprite bat;
  private double timer;
  private System.Random random;
  private double animationTimer;

  public BatIdleState(BatSprite bat) {
    this.bat = bat;
    this.random = new System.Random();

    this.bat.Velocity = Vector2.Zero;

    this.bat.CurrentSourceRectangles = new List<Rectangle> {
      //new Rectangle(3, 20, 25, 11),
      new Rectangle(35, 5, 27, 22),
      new Rectangle(66, 6, 29, 15),
      new Rectangle(97, 1, 31, 21),
    };
    this.bat.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) {
    animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
    if (animationTimer > 0.2) {
      bat.CurrentFrame++;
      if (bat.CurrentFrame >= bat.CurrentSourceRectangles.Count) {
        bat.CurrentFrame = 0;
      }
      animationTimer = 0;
    }
    timer += gameTime.ElapsedGameTime.TotalSeconds;
    if (timer > 2.0) {
      bat.ChangeState(new BatMoveState(bat));
    }
  }
}
