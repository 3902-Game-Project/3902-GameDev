using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class SwapWeaponCommand(Player player) : IGPCommand {
  public void Execute() => player.Inventory.SwapActiveWeapon();
}
