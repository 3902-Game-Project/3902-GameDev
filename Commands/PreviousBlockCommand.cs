using GameProject.Interfaces;

namespace GameProject.Commands;

public class PreviousBlockCommand(Game1 game) : ICommand {
  public void Execute() {
    game.BlockNumber--;

    if (game.BlockNumber == 0) { game.BlockNumber = game.Blocks.Count; }
  }
}
