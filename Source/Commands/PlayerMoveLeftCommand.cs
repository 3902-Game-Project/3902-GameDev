using GameProject.Interfaces;
using GameProject.PlayerSpace;
namespace GameProject.Commands;

public class PlayerMoveLeftCommand(Player player) : ICommand {
  public void Execute() {
    player.MoveLeft();
  }
}
