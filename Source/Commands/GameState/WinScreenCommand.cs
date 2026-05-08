using GameProject.GameStates;

namespace GameProject.Commands;

internal class WinScreenCommand(Game1 game) : IGPCommand {
  public void Execute() {
    game.StateMachine.ChangeState(GameState.StateWin);
  }
}
