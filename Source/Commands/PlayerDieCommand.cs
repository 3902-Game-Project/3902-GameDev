using GameProject.Interfaces;
using GameProject.PlayerSpace;

namespace GameProject.Commands;

public class PlayerDieCommand(Player player) : ICommand {
  public void Execute() {
    player.Die();
  }
}
