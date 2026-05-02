using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerDieCommand(Player player) : IGPCommand {
  public void Execute() {
    player.Die();
  }
}
