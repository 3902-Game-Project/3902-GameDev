using GameProject.Interfaces;
using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerUseItemCommand(Player player, UseType useType) : ICommand {
  public void Execute() {
    player.UseItem(useType);
  }
}
