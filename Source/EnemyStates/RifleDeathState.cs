using System.Collections.Generic;
using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class RifleDeathState : IRifleState {
  private RifleSprite rifleman;

  private double animationTimer;
  private double timePerFrame = 0.15;

  private double deadHoldTimer;
  private double timeToHoldLastFrame = 2.0;

  private bool isAnimationFinished = false;

  public RifleDeathState(RifleSprite rifleman) {
    this.rifleman = rifleman;


    this.rifleman.Velocity = Vector2.Zero;

    this.rifleman.CurrentSourceRectangles = new List<Rectangle> {
        new(11,9,21,28),
        new(73,11,23,26),
        new(135,16,33,21),
        new(198,20,40,17),
        new(260,22,40,15),
        new(323,23,39,14),
        new(385,25,40,12),
    };
    this.rifleman.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) {
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

    if (!isAnimationFinished) {
      animationTimer += dt;
      if (animationTimer >= timePerFrame) {
        if (rifleman.CurrentFrame < rifleman.CurrentSourceRectangles.Count - 1) {
          rifleman.CurrentFrame++;
          animationTimer = 0;
        } else {
          isAnimationFinished = true;
        }
      }
    }
    else {
      deadHoldTimer += dt;
      if (deadHoldTimer >= timeToHoldLastFrame) {
        rifleman.CurrentSourceRectangles.Clear();
      }
    }
  }
}
