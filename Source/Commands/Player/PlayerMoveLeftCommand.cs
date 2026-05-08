using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerMoveLeftCommand(Player player) : IGPCommand {
  internal void Execute() {
    player.MoveLeft();
  }
}
