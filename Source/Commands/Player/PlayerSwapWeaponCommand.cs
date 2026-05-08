using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerSwapWeaponCommand(Player player) : IGPCommand {
  internal void Execute() => player.Inventory.SwapActiveWeapon();
}
