using System.Collections.Generic;
using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class RifleAttackState : IRifleState {
  private RifleSprite rifle;
  private double stateTimer;
  private double animationTimer;
  private double timePerFrame = 0.15;

  public RifleAttackState(RifleSprite Rifle) {
    this.rifle = Rifle;

    this.rifle.Velocity = Vector2.Zero;

    this.rifle.CurrentSourceRectangles = new List<Rectangle> {
      new(8, 64, 44, 64),
      new(72, 62, 44, 62),
      new(136, 64, 44, 64),
      new(264, 68, 48, 60),
      new(328, 68, 48, 60),
    };
    this.rifle.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) {
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
    animationTimer += dt;
    if (animationTimer >= timePerFrame) {
      if (rifle.CurrentFrame < rifle.CurrentSourceRectangles.Count - 1) {
        rifle.CurrentFrame++;
        animationTimer = 0;
      }
    }
    stateTimer += dt;
    if (stateTimer > 1.0) {
      rifle.ChangeState(new RifleIdleState(rifle));
    }
  }
}
