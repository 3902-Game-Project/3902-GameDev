using System;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.RiflemanStates;

internal class RifleIdleState : IEnemyState {
  private readonly Rifleman rifle;
  private double timer, animationTimer;
  private readonly Random random = new();

  public RifleIdleState(Rifleman rifle) {
    this.rifle = rifle;
    this.rifle.Velocity = Vector2.Zero;
    this.rifle.CurrentSourceRectangles = [new(71, 130, 23, 28), new(134, 130, 23, 28), new(196, 130, 23, 28), new(259, 130, 23, 28)];
    this.rifle.CurrentFrame = 0;
  }

  public void Update(double deltaTime) {
    animationTimer += deltaTime;
    if (animationTimer > 0.2) {
      rifle.CurrentFrame = (rifle.CurrentFrame + 1) % rifle.CurrentSourceRectangles.Count;
      animationTimer = 0;
    }

    timer += deltaTime;
    if (timer > 1.0) rifle.CurrentState = SelectState();
  }

  private IEnemyState SelectState() {
    float xDistanceFromTarget = Math.Abs(rifle.Target.X - rifle.Position.X);
    float yDistanceFromTarget = Math.Abs(rifle.Target.Y - rifle.Position.Y);
    if (yDistanceFromTarget <= 25 || xDistanceFromTarget <= 25)
      return new RifleAttackState(rifle);

    return new RifleWanderState(rifle);
  }
}
