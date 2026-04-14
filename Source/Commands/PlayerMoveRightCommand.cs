using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerMoveRightCommand(Player player) : IGPCommand {
  public void Execute() {
    player.MoveRight();
  }
}
