using GameProject.Controllers;
using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerUseKeyCommand(Player player, UseType useType) : IGPCommand {
  internal void Execute() {
    player.UseKey(useType);
  }
}
