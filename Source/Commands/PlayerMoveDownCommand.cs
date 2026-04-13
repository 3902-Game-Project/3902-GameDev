using GameProject.GlobalInterfaces;
using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerMoveDownCommand(Player player) : ICommand {
  public void Execute() {
    player.MoveDown();
  }
}
