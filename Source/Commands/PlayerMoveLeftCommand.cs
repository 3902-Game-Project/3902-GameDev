using GameProject.GlobalInterfaces;
using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerMoveLeftCommand(Player player) : ICommand {
  public void Execute() {
    player.MoveLeft();
  }
}
