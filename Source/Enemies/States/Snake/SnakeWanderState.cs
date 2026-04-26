using GameProject.Enemies.States;

namespace GameProject.Enemies.SnakeStates;

internal class SnakeWanderState(Snake snake) : AEnemyMoveState(snake, [new(10, 84, 13, 13), new(43, 84, 13, 13), new(75, 84, 13, 13), new(105, 84, 14, 13), new(136, 84, 14, 13), new(166, 85, 15, 12), new(198, 85, 16, 12), new(232, 85, 15, 12), new(266, 85, 12, 12), new(298, 85, 12, 12)], 100f) {
  protected override void TransitionToNextState() {
    enemy.CurrentState = new SnakeIdleState((Snake) enemy);
  }
}
