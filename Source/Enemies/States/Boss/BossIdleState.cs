using System;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.BossStates;

internal class BossIdleState : IEnemyState {
  private readonly Boss boss;
  private double timer = 0.0, animationTimer = 0.0;

  public BossIdleState(Boss boss) {
    this.boss = boss;
    this.boss.Velocity = Vector2.Zero;
    // Placeholder idle frames
    this.boss.CurrentSourceRectangles = [new(0, 0, 64, 64), new(64, 0, 64, 64)];
    this.boss.CurrentFrame = 0;
  }

  public void Update(double deltaTime) {
    animationTimer += deltaTime;
    if (animationTimer > 0.2) {
      boss.CurrentFrame = (boss.CurrentFrame + 1) % boss.CurrentSourceRectangles.Count;
      animationTimer = 0;
    }

    timer += deltaTime;
    if (timer > 1.5) {
      boss.CurrentState = SelectState();
    }
  }

  private IEnemyState SelectState() {
    // Simple placeholder logic: 
    // If the player is close, attack. Otherwise, wander.
    float xDistanceFromTarget = Math.Abs(boss.Target.X - boss.Position.X);
    float yDistanceFromTarget = Math.Abs(boss.Target.Y - boss.Position.Y);

    if (xDistanceFromTarget <= 300 && yDistanceFromTarget <= 150) {
      return new BossAttackState(boss);
    }

    return new BossWanderState(boss);
  }
}
