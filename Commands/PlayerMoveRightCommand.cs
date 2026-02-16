using GameProject.Interfaces;

namespace GameProject.Commands;

public class PlayerMoveRightCommand(Game1 game) : ICommand {
  public void Execute() {
    game.Player.MoveRight();
  }
}
