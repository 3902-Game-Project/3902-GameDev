using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerMoveUpCommand(Player player) : IGPCommand {
  public void Execute() {
    player.MoveUp();
  }
}
