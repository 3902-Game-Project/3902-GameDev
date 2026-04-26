using System;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.ShotgunnerStates;

internal class ShotgunnerIdleState : IEnemyState {
  private readonly Shotgunner shotgunner;
  private double timer = 0.0, animationTimer = 0.0;
  private readonly Random random = new();

  public ShotgunnerIdleState(Shotgunner shotgunner) {
    this.shotgunner = shotgunner;
    this.shotgunner.Velocity = Vector2.Zero;
    this.shotgunner.CurrentSourceRectangles = [new(21, 339, 32, 39), new(98, 337, 32, 41), new(174, 339, 32, 39), new(251, 341, 32, 37)];
    this.shotgunner.CurrentFrame = 0;
  }

  public void Update(double deltaTime) {
    animationTimer += deltaTime;
    if (animationTimer > 0.2) {
      shotgunner.CurrentFrame = (shotgunner.CurrentFrame + 1) % shotgunner.CurrentSourceRectangles.Count;
      animationTimer = 0;
    }

    timer += deltaTime;
    if (timer > 1.0) shotgunner.CurrentState = SelectState();
  }

  private IEnemyState SelectState() {
    float xDistanceFromTarget = Math.Abs(shotgunner.Target.X - shotgunner.Position.X);
    float yDistanceFromTarget = Math.Abs(shotgunner.Target.Y - shotgunner.Position.Y);
    if (xDistanceFromTarget <= 250 && yDistanceFromTarget <= 100
      || yDistanceFromTarget <= 250 && xDistanceFromTarget <= 100)
      return new ShotgunnerAttackState(shotgunner);

    return new ShotgunnerWanderState(shotgunner);
  }
}
