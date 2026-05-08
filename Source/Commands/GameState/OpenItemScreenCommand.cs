using GameProject.GameStates;

namespace GameProject.Commands;

internal class OpenItemScreenCommand(Game1 game) : IGPCommand {
  internal void Execute() {
    game.StateMachine.ChangeStateWithoutFading(GameState.StateItemScreen);
  }
}
