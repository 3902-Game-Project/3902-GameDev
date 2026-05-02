using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerMoveDownCommand(Player player) : IGPCommand {
  public void Execute() {
    player.MoveDown();
  }
}
