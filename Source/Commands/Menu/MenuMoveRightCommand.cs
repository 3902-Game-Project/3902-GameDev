using GameProject.GameStates;

namespace GameProject.Commands;

internal class MenuMoveRightCommand(StateItemScreenType screen) : IGPCommand {
  public void Execute() => screen.MoveCursorRight();
}
