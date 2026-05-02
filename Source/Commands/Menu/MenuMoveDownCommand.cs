using GameProject.GameStates;

namespace GameProject.Commands;

internal class MenuMoveDownCommand(StateItemScreenType state) : IGPCommand {
  public void Execute() => state.MoveCursorDown();
}
