using System;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.TumbleweedStates;

internal class TumbleweedIdleState : IEnemyState {
  private readonly Tumbleweed tumbleweed;
  private readonly Random random = new();
  private double idleTimer, animationTimer;
  private readonly double idleDuration;

  public TumbleweedIdleState(Tumbleweed tumbleweed) {
    this.tumbleweed = tumbleweed;
    this.tumbleweed.Velocity = Vector2.Zero;
    this.tumbleweed.CurrentSourceRectangles = [new(159, 217, 121, 110)];
    this.tumbleweed.CurrentFrame = 0;
    idleDuration = 0.5 + random.NextDouble();
  }

  public void Update(GameTime gameTime) {
    float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
    animationTimer += dt;
    if (animationTimer >= 0.2) {
      tumbleweed.CurrentFrame = (tumbleweed.CurrentFrame + 1) % tumbleweed.CurrentSourceRectangles.Count;
      animationTimer = 0;
    }

    idleTimer += dt;
    if (idleTimer > idleDuration) tumbleweed.CurrentState = new TumbleweedWanderState(tumbleweed);
  }
}
