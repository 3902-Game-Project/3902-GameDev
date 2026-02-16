using GameProject.Interfaces;

namespace GameProject.Commands;

public class PlayerMoveDownCommand(Game1 game) : ICommand {
  public void Execute() {
    game.Player.MoveDown();
  }
}
