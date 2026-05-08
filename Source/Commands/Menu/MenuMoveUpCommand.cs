using GameProject.GameStates;

namespace GameProject.Commands;

internal class MenuMoveUpCommand(StateItemScreenType state) : IGPCommand {
  internal void Execute() => state.MoveCursorUp();
}
