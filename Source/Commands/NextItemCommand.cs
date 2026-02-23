using GameProject.Interfaces;

namespace GameProject.Commands;

public class NextItemCommand(Game1 game) : ICommand {
  public void Execute() {
    game.StateGame.ItemNumber++;

    if (game.StateGame.ItemNumber >= game.StateGame.Items.Count) { game.StateGame.ItemNumber = 0; }
  }
}
