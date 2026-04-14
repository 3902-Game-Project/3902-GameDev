using GameProject.GameStates;

namespace GameProject.Commands;

internal class MenuMoveLeftCommand(StateItemScreenType screen) : IGPCommand {
  public void Execute() => screen.MoveCursorLeft();
}

internal class MenuMoveRightCommand(StateItemScreenType screen) : IGPCommand {
  public void Execute() => screen.MoveCursorRight();
}

internal class MenuEquipCommand(StateItemScreenType screen) : IGPCommand {
  public void Execute() => screen.EquipSelectedWeapon();
}
