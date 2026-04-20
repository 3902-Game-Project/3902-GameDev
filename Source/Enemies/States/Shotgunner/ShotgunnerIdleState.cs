using System;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.ShotgunnerStates;

internal class ShotgunnerIdleState : IEnemyState {
  private readonly Shotgunner shotgunner;
  private double timer, animationTimer;
  private readonly Random random = new();

  public ShotgunnerIdleState(Shotgunner shotgunner) {
    this.shotgunner = shotgunner;
    this.shotgunner.Velocity = Vector2.Zero;
    this.shotgunner.CurrentSourceRectangles = [new(21, 339, 32, 39), new(98, 337, 32, 41), new(174, 339, 32, 39), new(251, 341, 32, 37)];
    this.shotgunner.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) {
    animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
    if (animationTimer > 0.2) {
      shotgunner.CurrentFrame = (shotgunner.CurrentFrame + 1) % shotgunner.CurrentSourceRectangles.Count;
      animationTimer = 0;
    }

    timer += gameTime.ElapsedGameTime.TotalSeconds;
    if (timer > 1.0) shotgunner.CurrentState = random.Next(0, 2) == 0 ? new ShotgunnerAttackState(shotgunner) : new ShotgunnerWanderState(shotgunner);
  }
}
