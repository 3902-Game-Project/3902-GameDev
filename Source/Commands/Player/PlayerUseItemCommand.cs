using GameProject.Controllers;
using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerUseItemCommand(Player player, UseType useType) : IGPCommand {
  internal void Execute() {
    player.UseItem(useType);
  }
}
