using GameProject.Interfaces;

namespace GameProject.Commands;

public class NextBlockCommand(Game1 game) : ICommand {
  public void Execute() {
    game.BlockNumber++;

    if (game.BlockNumber == game.Blocks.Count) { game.BlockNumber = 0; }
  }
}
