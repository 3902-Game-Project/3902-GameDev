using GameProject.GameStates;

namespace GameProject.Commands;

internal class ReturnToGameNoFadeCommand(Game1 game) : IGPCommand {
  internal void Execute() {
    game.StateMachine.ChangeStateWithoutFading(GameState.StateGame);
  }
}
