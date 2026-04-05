using GameProject.Interfaces;
using GameProject.PlayerSpace;

namespace GameProject.Commands;

public class PlayerMoveRightCommand(Player player) : ICommand {
  public void Execute() {
    player.MoveRight();
  }
}
