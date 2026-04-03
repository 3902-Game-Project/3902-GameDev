using GameProject.Interfaces;
using GameProject.PlayerSpace;
namespace GameProject.Commands;

public class PlayerMoveUpCommand(Player player) : ICommand {
  public void Execute() {
    player.MoveUp();
  }
}
