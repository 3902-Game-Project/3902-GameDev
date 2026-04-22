using GameProject.Enemies.States;

namespace GameProject.Enemies.BatStates;

internal class BatMoveState(Bat bat) : AEnemyMoveState(bat, [new(38, 97, 17, 21), new(70, 102, 17, 15), new(102, 102, 15, 21)], 100f) {
  protected override void TransitionToNextState() {
    enemy.CurrentState = new BatIdleState((Bat) enemy);
  }
}
