using GameProject.Controllers;
using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerUseKeyCommand(Player player, UseType useType) : IGPCommand {
  public void Execute() {
    player.UseKey(useType);
  }
}
