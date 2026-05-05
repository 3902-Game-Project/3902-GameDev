using System;

namespace GameProject.PlayerSpace.States;

internal class PlayerStateMachine {
  public IPlayerState StaticState { get; }
  public IPlayerState MovingState { get; }
  public IPlayerState UseItemState { get; }
  public IPlayerState DeadState { get; }
  public IPlayerState CurrentState { get; private set; }

  public PlayerStateMachine(Player player, Action onLoss) {
    StaticState = new PlayerStaticState(player);
    MovingState = new PlayerMovingState(player);
    UseItemState = new PlayerUseItemState(player);
    DeadState = new PlayerDeadState(player, onLoss);

    // Initialize the default state
    CurrentState = StaticState;
  }
  public void ChangeState(IPlayerState newState) {
    CurrentState = newState;
  }
}
