using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerMoveDownCommand(Player player) : IGPCommand {
  internal void Execute() {
    player.MoveDown();
  }
}
