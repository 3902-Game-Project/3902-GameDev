using GameProject.Items;
using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class ReloadWeaponCommand(Player player) : IGPCommand {
  public void Execute() {
    if (player.Inventory.ActiveItem is ABaseGun gun) {
      gun.StartReload();
    }
  }
}
