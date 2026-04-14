using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerMoveLeftCommand(Player player) : IGPCommand {
  public void Execute() {
    player.MoveLeft();
  }
}
