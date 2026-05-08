using GameProject.GameStates;

namespace GameProject.Commands;

internal class MenuMoveRightCommand(StateItemScreenType screen) : IGPCommand {
  internal void Execute() => screen.MoveCursorRight();
}
