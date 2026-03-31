using GameProject.Enums;
using GameProject.Interfaces;

namespace GameProject.Commands;
public class SwitchItemCommand(Player player, ItemCategory category) : ICommand {
  public void Execute() {
    player.Inventory.SwitchActiveItem(category);
  }
}
