using System.Collections.Generic;
using GameProject.Enemies;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class ShotgunnerDeathState : IShotgunnerState {
  private ShotgunnerSprite shotgunner;

  private double animationTimer;
  private double timePerFrame = 0.15;

  private double deadHoldTimer;
  private double timeToHoldLastFrame = 2.0;

  private bool isAnimationFinished = false;

  public ShotgunnerDeathState(ShotgunnerSprite shotgunner) {
    this.shotgunner = shotgunner;
    this.shotgunner.Velocity = Vector2.Zero;

    this.shotgunner.CurrentSourceRectangles = new List<Rectangle> {
      new(14, 568, 39, 40),
      new(100, 573, 37, 35),
      new(174, 576, 42, 32),
      new(246, 585, 51, 23)
    };
    this.shotgunner.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) {
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

    if (!isAnimationFinished) {
      animationTimer += dt;
      if (animationTimer >= timePerFrame) {
        if (shotgunner.CurrentFrame < shotgunner.CurrentSourceRectangles.Count - 1) {
          shotgunner.CurrentFrame++;
          animationTimer = 0;
        } else {
          isAnimationFinished = true;
        }
      }
    } else {
      deadHoldTimer += dt;
      if (deadHoldTimer >= timeToHoldLastFrame) {
        shotgunner.CurrentSourceRectangles.Clear();
      }
    }
  }
}
