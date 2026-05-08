using GameProject.Items;
using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerReloadWeaponCommand(Player player) : IGPCommand {
  internal void Execute() {
    if (player.Inventory.ActiveItem is ABaseGun gun) {
      gun.StartReload();
    }
  }
}
