using System;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.BossStates;

internal class BossIdleState : IEnemyState {
  private readonly Boss boss;
  private double timer = 0.0, animationTimer = 0.0;
  private readonly Random random = new();

  public BossIdleState(Boss boss) {
    this.boss = boss;
    this.boss.Velocity = Vector2.Zero;
    this.boss.CurrentSourceRectangles = [
      new(0, 38, 56, 48), new(56, 38, 56, 48), new(112, 38, 56, 48),
      new(168, 38, 56, 48), new(224, 38, 56, 48), new(280, 38, 56, 48),
    ];
    this.boss.CurrentFrame = 0;
  }

  public void Update(double deltaTime) {
    animationTimer += deltaTime;
    if (animationTimer > 0.2) {
      boss.CurrentFrame = (boss.CurrentFrame + 1) % boss.CurrentSourceRectangles.Count;
      animationTimer = 0;
    }

    timer += deltaTime;
    if (timer > 0.5) {
      boss.CurrentState = SelectState();
    }
  }

  private IEnemyState SelectState() {
    // Rely purely on the base enemy's Target property!
    float xDist = Math.Abs(boss.Target.X - boss.Position.X);
    float yDist = Math.Abs(boss.Target.Y - boss.Position.Y);
    int roll = random.Next(100);

    if (boss.PhaseTwoTriggered && roll < 15) {
      return new BossSpecialAttackState(boss);
    }

    // RIFLEMAN LOGIC: If lined up, attack!
    if (xDist <= 25 || yDist <= 25) {
      if (roll < 50) return new BossAttackState(boss);
      else return new BossAttack2State(boss);
    }

    // Otherwise, wander until he lines up
    return new BossWanderState(boss);
  }
}
