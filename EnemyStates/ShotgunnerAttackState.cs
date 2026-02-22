using System.Collections.Generic;
using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class ShotgunnerAttackState : IShotgunnerState {
  private ShotgunnerSprite shotgunner;
  private double stateTimer;
  private double animationTimer;
  private double timePerFrame = 0.15;

  public ShotgunnerAttackState(ShotgunnerSprite shotgunner) {
    this.shotgunner = shotgunner;

    this.shotgunner.Velocity = Vector2.Zero;

    this.shotgunner.CurrentSourceRectangles = new List<Rectangle> {
      new(23, 418, 35, 37),
      new(100, 415, 32, 40),
      new(174, 415, 39, 40),
    };
    this.shotgunner.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) {
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
    animationTimer += dt;
    if (animationTimer >= timePerFrame) {
      if (shotgunner.CurrentFrame < shotgunner.CurrentSourceRectangles.Count - 1) {
        shotgunner.CurrentFrame++;
        animationTimer = 0;
      }
    }
    stateTimer += dt;
    if (stateTimer > 1.0) {
      shotgunner.ChangeState(new ShotgunnerIdleState(shotgunner));
    }
  }
}
