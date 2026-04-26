using GameProject.Enemies.States;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.TumbleweedStates;

internal class TumbleweedWanderState(Tumbleweed tumbleweed) :
  AEnemyMoveState(
    tumbleweed,
    [
      new(36, 41, 108, 106),
      new(202, 42, 109, 105),
      new(366, 42, 109, 105),
      new(533, 41, 107, 106)
    ],
    75f,
    // Notice the `true` parameter! This locks the Y-axis so it only rolls left/right.
    lockYAxis: true
  ) {
  protected override void TransitionToNextState() {
    enemy.CurrentState = new TumbleweedIdleState((Tumbleweed) enemy);
  }

  public override void Update(double deltaTime) {
    base.Update(deltaTime);
    Vector2 horizontalVelocity = new(enemy.Velocity.X, 0f);
    enemy.Velocity = horizontalVelocity == Vector2.Zero
      ? Vector2.Zero
      : Vector2.Normalize(horizontalVelocity) * enemy.Velocity.Length();
  }
}
