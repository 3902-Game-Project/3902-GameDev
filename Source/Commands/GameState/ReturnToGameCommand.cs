using GameProject.GameStates;

namespace GameProject.Commands;

internal class ReturnToGameCommand(Game1 game) : IGPCommand {
  internal void Execute() {
    game.StateMachine.ChangeState(GameState.StateGame);
  }
}
