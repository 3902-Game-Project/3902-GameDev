using GameProject.Interfaces;

namespace GameProject.Commands;

public class PlayerMoveLeftCommand(Game1 game) : ICommand {
  public void Execute() {
    game.Player.MoveLeft();
  }
}
