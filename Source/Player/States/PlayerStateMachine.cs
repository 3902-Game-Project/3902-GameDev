using System;
using GameProject.Level;

namespace GameProject.PlayerSpace.States;

internal class PlayerStateMachine {
  public IPlayerState StaticState { get; }
  public IPlayerState MovingState { get; }
  public IPlayerState UseItemState { get; }
  public IPlayerState DeadState { get; }
  public IPlayerState CurrentState { get; private set; }

  public PlayerStateMachine(Player player, CurrentLevelGetter GetCurrentLevel, Action onLoss) {
    StaticState = new PlayerStaticState(player, GetCurrentLevel);
    MovingState = new PlayerMovingState(player, GetCurrentLevel);
    UseItemState = new PlayerUseItemState(player, GetCurrentLevel);
    DeadState = new PlayerDeadState(player, GetCurrentLevel, onLoss);

    // Initialize the default state
    CurrentState = StaticState;
  }

  public void ChangeState(IPlayerState newState) {
    CurrentState = newState;
  }
}
