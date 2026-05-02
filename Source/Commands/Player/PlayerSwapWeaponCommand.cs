using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerSwapWeaponCommand(Player player) : IGPCommand {
  public void Execute() => player.Inventory.SwapActiveWeapon();
}
