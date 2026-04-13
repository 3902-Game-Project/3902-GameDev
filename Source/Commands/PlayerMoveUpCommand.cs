using GameProject.GlobalInterfaces;
using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerMoveUpCommand(Player player) : ICommand {
  public void Execute() {
    player.MoveUp();
  }
}
