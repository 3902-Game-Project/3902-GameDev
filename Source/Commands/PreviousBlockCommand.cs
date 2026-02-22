using GameProject.Interfaces;

namespace GameProject.Commands;

public class PreviousBlockCommand(Game1 game) : ICommand {
  public void Execute() {
    game.StateGame.BlockNumber--;

    if (game.StateGame.BlockNumber < 0) { game.StateGame.BlockNumber = game.StateGame.Blocks.Count - 1; }
  }
}
