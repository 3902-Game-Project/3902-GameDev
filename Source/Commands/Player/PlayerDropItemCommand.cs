using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerDropItemCommand(Player player) : IGPCommand {
  public void Execute() {
    player.Inventory.DropCurrentItem();
  }
}
