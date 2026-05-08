using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerDropItemCommand(Player player) : IGPCommand {
  internal void Execute() {
    player.Inventory.DropCurrentItem();
  }
}
