using GameProject.GameStates;

namespace GameProject.Commands;

internal class MenuEquipCommand(StateItemScreenType screen) : IGPCommand {
  internal void Execute() => screen.EquipSelectedWeapon();
}
