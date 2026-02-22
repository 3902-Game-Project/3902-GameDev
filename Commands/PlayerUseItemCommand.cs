using GameProject.Interfaces;

namespace GameProject.Commands;

public class PlayerUseItemCommand(Player player) : ICommand {
  public void Execute() {
    player.UseItem();
  }
}
