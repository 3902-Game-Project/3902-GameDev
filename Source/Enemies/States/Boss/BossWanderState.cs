using GameProject.Enemies.States;

namespace GameProject.Enemies.BossStates;

internal class BossWanderState(Boss boss) : AEnemyMoveState(boss, [new(0, 64, 64, 64), new(64, 64, 64, 64)], 75f) {

  protected override void TransitionToNextState() {
    // Return to idle after wandering
    enemy.CurrentState = new BossIdleState((Boss) enemy);
  }
}
