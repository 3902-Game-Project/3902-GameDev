using System;

namespace GameProject.PlayerSpace.States;

internal class PlayerStateMachine {
  internal IPlayerState StaticState { get; }
  internal IPlayerState MovingState { get; }
  internal IPlayerState UseItemState { get; }
  internal IPlayerState DeadState { get; }
  internal IPlayerState CurrentState { get; private set; }

  internal PlayerStateMachine(Player player, Action onLoss) {
    StaticState = new PlayerStaticState(player);
    MovingState = new PlayerMovingState(player);
    UseItemState = new PlayerUseItemState(player);
    DeadState = new PlayerDeadState(player, onLoss);

    // Initialize the default state
    CurrentState = StaticState;
  }

  internal void ChangeState(IPlayerState newState) {
    CurrentState = newState;
  }
}
