using GameProject.Interfaces;
using GameProject.PlayerSpace;

namespace GameProject.Commands;

public class PlayerMoveDownCommand(Player player) : ICommand {
  public void Execute() {
    player.MoveDown();
  }
}
