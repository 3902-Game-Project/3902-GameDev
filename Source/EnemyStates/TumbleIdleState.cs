using System;
using GameProject.Enemies;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class TumbleIdleState : ITumbleState {
  private readonly TumbleSprite tumbleweed;
  private readonly Random random;
  private double idleTimer;
  private readonly double idleDuration;

  private double animationTimer;
  private int currentFrameIndex;

  public TumbleIdleState(TumbleSprite tumbleweed) {
    this.tumbleweed = tumbleweed;
    random = new Random();

    this.tumbleweed.Velocity = Vector2.Zero;

    this.tumbleweed.CurrentSourceRectangles = [
      new(159, 217, 121, 110)
      //new(383, 227, 137, 106) death frames
    ];
    this.tumbleweed.CurrentFrame = 0;

    idleTimer = 0;
    idleDuration = 0.5 + random.NextDouble();
  }

  public void Update(GameTime gameTime) {
    float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

    animationTimer += dt;
    if (animationTimer >= 0.2) {
      currentFrameIndex++;
      if (currentFrameIndex >= tumbleweed.CurrentSourceRectangles.Count) {
        currentFrameIndex = 0;
      }

      tumbleweed.CurrentFrame = currentFrameIndex;
      animationTimer = 0;
    }

    idleTimer += dt;

    if (idleTimer > idleDuration) {
      tumbleweed.ChangeState(new TumbleMoveState(tumbleweed));
    }
  }
}
