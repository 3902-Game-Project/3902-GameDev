using GameProject.Interfaces;
using GameProject.GameStates;

namespace GameProject.Commands;

public class MenuMoveLeftCommand(StateItemScreenType screen) : ICommand {
  public void Execute() => screen.MoveCursorLeft();
}

public class MenuMoveRightCommand(StateItemScreenType screen) : ICommand {
  public void Execute() => screen.MoveCursorRight();
}

public class MenuEquipCommand(StateItemScreenType screen) : ICommand {
  public void Execute() => screen.EquipSelectedWeapon();
}
