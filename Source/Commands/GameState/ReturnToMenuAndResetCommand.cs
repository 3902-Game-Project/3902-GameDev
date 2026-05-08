using GameProject.GameStates;

namespace GameProject.Commands;

internal class ReturnToMenuAndResetCommand(Game1 game) : IGPCommand {
  public void Execute() {
    game.StateMachine.ChangeState(GameState.StateMenu);
    game.StateMachine.ResetGameState();
  }
}
