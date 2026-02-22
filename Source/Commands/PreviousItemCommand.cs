using GameProject.Interfaces;

namespace GameProject.Commands;

public class PreviousItemCommand(Game1 game) : ICommand {
  public void Execute() {
    game.StateGame.ItemNumber--;

    if (game.StateGame.ItemNumber < 0) { game.StateGame.ItemNumber = game.StateGame.Items.Count - 1; }
  }
}
