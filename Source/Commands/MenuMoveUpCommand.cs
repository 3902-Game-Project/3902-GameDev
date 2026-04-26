using GameProject.GameStates;

namespace GameProject.Commands;

internal class MenuMoveUpCommand(StateItemScreenType state) : IGPCommand {
  public void Execute() => state.MoveCursorUp();
}
