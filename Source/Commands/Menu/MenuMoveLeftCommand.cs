using GameProject.GameStates;

namespace GameProject.Commands;

internal class MenuMoveLeftCommand(StateItemScreenType screen) : IGPCommand {
  internal void Execute() => screen.MoveCursorLeft();
}
