using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerDieCommand(Player player) : IGPCommand {
  internal void Execute() {
    player.Die();
  }
}
