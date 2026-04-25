using System;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.TumbleweedStates;

internal class TumbleweedIdleState : IEnemyState {
  private readonly Tumbleweed tumbleweed;
  private readonly Random random = new();
  private double idleTimer = 0.0, animationTimer = 0.0;
  private readonly double idleDuration;

  public TumbleweedIdleState(Tumbleweed tumbleweed) {
    this.tumbleweed = tumbleweed;
    this.tumbleweed.Velocity = Vector2.Zero;
    this.tumbleweed.CurrentSourceRectangles = [new(159, 217, 121, 110)];
    this.tumbleweed.CurrentFrame = 0;
    idleDuration = 0.5 + random.NextDouble();
  }

  public void Update(double deltaTime) {
    animationTimer += deltaTime;
    if (animationTimer >= 0.2) {
      tumbleweed.CurrentFrame = (tumbleweed.CurrentFrame + 1) % tumbleweed.CurrentSourceRectangles.Count;
      animationTimer = 0;
    }

    idleTimer += deltaTime;
    if (idleTimer > idleDuration) tumbleweed.CurrentState = new TumbleweedWanderState(tumbleweed);
  }
}
