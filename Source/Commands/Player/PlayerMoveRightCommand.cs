using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerMoveRightCommand(Player player) : IGPCommand {
  internal void Execute() {
    player.MoveRight();
  }
}
