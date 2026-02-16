using GameProject.Interfaces;

namespace GameProject.Commands;

public class PlayerMoveUpCommand(Game1 game) : ICommand {
  public void Execute() {
    game.Player.MoveUp();
  }
}
