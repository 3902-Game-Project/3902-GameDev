using GameProject.GameStates;

namespace GameProject.Commands;

internal class StartGameCommand(Game1 game) : IGPCommand {
  public void Execute() {
    game.StateMachine.ChangeState(GameState.StateGame);
  }
}
