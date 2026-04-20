using System;
using GameProject.Enemies.States;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.RiflemanStates;

internal class RifleWanderState(Rifleman rifle) : AEnemyMoveState(rifle, [new(71, 130, 23, 28), new(134, 130, 23, 28), new(196, 130, 23, 28), new(259, 130, 23, 28)], 100f) {
  protected override void TransitionToNextState() {
    enemy.CurrentState = new RifleIdleState((Rifleman) enemy);
  }
}
