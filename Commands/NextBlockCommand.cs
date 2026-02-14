using GameProject.Interfaces;

namespace GameProject.Commands;

public class NextBlockCommand(Game1 game) : ICommand {
  public void Execute() {
    game.StateGame.BlockNumber++;

    if (game.StateGame.BlockNumber >= game.StateGame.Blocks.Count) { game.StateGame.BlockNumber = 0; }
  }
}
