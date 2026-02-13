using GameProject.Interfaces;

namespace GameProject.Commands;

public class NextBlockCommand(Game1 game) : ICommand {
  public void Execute() {
    game.ChangeState(game.StateGame);
  }
}
