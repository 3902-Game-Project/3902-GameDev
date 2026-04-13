using GameProject.GlobalInterfaces;
using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerDieCommand(Player player) : ICommand {
  public void Execute() {
    player.Die();
  }
}
