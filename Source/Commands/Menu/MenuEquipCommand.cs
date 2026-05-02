using GameProject.GameStates;

namespace GameProject.Commands;

internal class MenuEquipCommand(StateItemScreenType screen) : IGPCommand {
  public void Execute() => screen.EquipSelectedWeapon();
}
