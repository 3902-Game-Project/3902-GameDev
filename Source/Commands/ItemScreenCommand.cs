using GameProject.Interfaces;

namespace GameProject.Commands;

public class ItemScreenCommand(Game1 game) : ICommand {
  public void Execute() {
    game.ChangeState(game.StateItem);
  }
}
