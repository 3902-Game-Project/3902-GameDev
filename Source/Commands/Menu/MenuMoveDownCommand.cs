using GameProject.GameStates;

namespace GameProject.Commands;

internal class MenuMoveDownCommand(StateItemScreenType state) : IGPCommand {
  internal void Execute() => state.MoveCursorDown();
}
