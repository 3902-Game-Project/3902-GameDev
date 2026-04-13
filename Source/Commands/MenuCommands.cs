using GameProject.Interfaces;
using GameProject.GameStates;

namespace GameProject.Commands;

internal class MenuMoveLeftCommand(StateItemScreenType screen) : ICommand {
  public void Execute() => screen.MoveCursorLeft();
}

internal class MenuMoveRightCommand(StateItemScreenType screen) : ICommand {
  public void Execute() => screen.MoveCursorRight();
}

internal class MenuEquipCommand(StateItemScreenType screen) : ICommand {
  public void Execute() => screen.EquipSelectedWeapon();
}
