using System;
using GameProject.Globals;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.BossStates;

internal class BossIdleState : IEnemyState {
  private const double ANIMATION_INTERVAL = 0.2;
  private const double IDLE_DURATION = 0.5;
  private const int PHASE_TWO_CHANCE = 15;
  private const int ATTACK_1_CHANCE = 50;
  private const int ROLL_MAX = 100;

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
    if (animationTimer > ANIMATION_INTERVAL) {
      boss.CurrentFrame = (boss.CurrentFrame + 1) % boss.CurrentSourceRectangles.Count;
      animationTimer = 0;
    }

    timer += deltaTime;
    if (timer > IDLE_DURATION) {
      boss.CurrentState = SelectState();
    }
  }

  private IEnemyState SelectState() {
    // Rely purely on the base enemy's Target property!
    float xDist = Math.Abs(boss.Target.X - boss.Position.X);
    float yDist = Math.Abs(boss.Target.Y - boss.Position.Y);
    int roll = random.Next(ROLL_MAX);

    if (boss.PhaseTwoTriggered && roll < PHASE_TWO_CHANCE) {
      return new BossSpecialAttackState(boss);
    }

    // RIFLEMAN LOGIC: If lined up, attack!
    if (xDist <= Constants.BOSS_ALIGNMENT_THRESHOLD || yDist <= Constants.BOSS_ALIGNMENT_THRESHOLD) {
      if (roll < ATTACK_1_CHANCE) return new BossAttackState(boss);
      else return new BossAttack2State(boss);
    }

    // Otherwise, wander until he lines up
    return new BossWanderState(boss);
  }
}
