using System;
using GameProject.Enemies.States;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.BossStates;

internal class BossWanderState : AEnemyMoveState {
  private readonly Boss boss;

  public BossWanderState(Boss boss) :
    base(
      boss,
      [
        new(0, 95, 56, 45),
        new(56, 95, 56, 45),
        new(112, 95, 56, 45),
        new(168, 95, 56, 45),
        new(224, 95, 56, 45),
        new(280, 95, 56, 45),
      ],
      120.0f
    ) {
    this.boss = boss;
    this.boss.CurrentFrame = 0;
  }

  public override void Update(double deltaTime) {
    base.Update(deltaTime);

    // Check distance on the X and Y axes individually using the built-in Target!
    float xDist = Math.Abs(boss.Position.X - boss.Target.X);
    float yDist = Math.Abs(boss.Position.Y - boss.Target.Y);

    // RIFLEMAN LOGIC: If lined up horizontally OR vertically, stop and attack!
    if (xDist <= 25 || yDist <= 25) {
      boss.CurrentState = new BossAttackState(boss);
    }
  }

  protected override void TransitionToNextState() {
    boss.CurrentState = new BossIdleState(boss);
  }
}
