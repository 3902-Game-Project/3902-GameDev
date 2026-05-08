using GameProject.GameStates;

namespace GameProject.Commands;

internal class OpenItemScreenCommand(Game1 game) : IGPCommand {
  public void Execute() {
    game.GameStateMachine.ChangeStateWithoutFading(GameState.StateItemScreen);
  }
}
