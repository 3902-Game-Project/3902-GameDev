using System;
using GameProject.Enemies.States;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.ShotgunnerStates;

internal class ShotgunnerWanderState(Shotgunner shotgunner) : AEnemyMoveState(shotgunner, [new(21, 339, 32, 39), new(98, 337, 32, 41), new(174, 339, 32, 39), new(251, 341, 32, 37)], 100f) {
  protected override void TransitionToNextState() {
    enemy.CurrentState = new ShotgunnerIdleState((Shotgunner) enemy);
  }
}
