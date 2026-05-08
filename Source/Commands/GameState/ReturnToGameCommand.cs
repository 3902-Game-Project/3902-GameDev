using GameProject.GameStates;

namespace GameProject.Commands;

internal class ReturnToGameCommand(Game1 game) : IGPCommand {
  public void Execute() {
    game.StateMachine.ChangeState(GameState.StateGame);
  }
}
