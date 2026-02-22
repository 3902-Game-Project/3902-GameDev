using GameProject.Interfaces;

namespace GameProject.Commands;

public class PlayerMoveUpCommand(Player player) : ICommand {
  public void Execute() {
    player.MoveUp();
  }
}
