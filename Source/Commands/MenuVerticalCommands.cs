using GameProject.GameStates;

namespace GameProject.Commands;

internal class MenuMoveUpCommand(StateItemScreenType state) : IGPCommand {
  public void Execute() => state.MoveCursorUp();
}

internal class MenuMoveDownCommand(StateItemScreenType state) : IGPCommand {
  public void Execute() => state.MoveCursorDown();
}
