using GameProject.Interfaces;

namespace GameProject.Commands;

public class PlayerDieCommand(Player player) : ICommand {
  public void Execute() {
    player.Die();
  }
}
