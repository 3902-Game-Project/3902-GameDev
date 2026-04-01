using System.Collections.Generic;
using GameProject.Enemies;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class BatDeathState : IBatState {
  private BatSprite bat;

  private double animationTimer;
  private double timePerFrame = 0.15;

  private double deadHoldTimer;
  private double timeToHoldLastFrame = 1.5;

  private bool isAnimationFinished = false;

  public BatDeathState(BatSprite bat) {
    this.bat = bat;

    this.bat.Velocity = Vector2.Zero;

    this.bat.CurrentSourceRectangles = [
      new(3, 20, 25, 11)
    ];
    this.bat.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) {
    float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
    if (!isAnimationFinished) {
      animationTimer += dt;
      if (animationTimer >= timePerFrame) {
        if (bat.CurrentFrame < bat.CurrentSourceRectangles.Count - 1) {
          bat.CurrentFrame++;
          animationTimer = 0;
        } else {
          isAnimationFinished = true;
        }
      } else {
        deadHoldTimer += dt;
        if (deadHoldTimer >= timeToHoldLastFrame) {
          bat.CurrentSourceRectangles.Clear();
        }
      }
    }
  }
}
