using GameProject.GameStates;

namespace GameProject.Commands;

internal class MenuMoveLeftCommand(StateItemScreenType screen) : IGPCommand {
  public void Execute() => screen.MoveCursorLeft();
}
