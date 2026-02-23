using System.Collections.Generic;
using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class TumbleDeathState : ITumbleState {
  private TumbleSprite tumbleweed;

  private double deadHoldTimer;
  private double timeToHoldLastFrame = 1.0;

  public TumbleDeathState(TumbleSprite tumbleweed) {
    this.tumbleweed = tumbleweed;

    this.tumbleweed.Velocity = Vector2.Zero;

    this.tumbleweed.CurrentSourceRectangles = new List<Rectangle> {
      new(383, 227, 137, 106)
    };
    this.tumbleweed.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) {
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
    deadHoldTimer += dt;

    if (deadHoldTimer >= timeToHoldLastFrame) {
      tumbleweed.CurrentSourceRectangles.Clear();
    }
  }
}
