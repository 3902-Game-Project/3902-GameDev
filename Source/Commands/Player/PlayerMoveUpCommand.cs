using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerMoveUpCommand(Player player) : IGPCommand {
  internal void Execute() {
    player.MoveUp();
  }
}
