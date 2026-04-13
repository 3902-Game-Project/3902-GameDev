using GameProject.GlobalInterfaces;
using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerMoveRightCommand(Player player) : ICommand {
  public void Execute() {
    player.MoveRight();
  }
}
