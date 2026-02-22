using GameProject.Interfaces;

namespace GameProject.Commands;

public class PlayerMoveRightCommand(Player player) : ICommand {
  public void Execute() {
    player.MoveRight();
  }
}
