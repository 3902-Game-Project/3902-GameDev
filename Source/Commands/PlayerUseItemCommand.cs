using GameProject.Interfaces;

namespace GameProject.Commands;

public class PlayerUseItemCommand(Player player, UseType useType) : ICommand {
  public void Execute() {
    player.UseItem(useType);
  }
}
