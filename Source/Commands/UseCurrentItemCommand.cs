using GameProject.Interfaces;

namespace GameProject.Commands;

public class UseCurrentItemCommand(Game1 game, UseType useType) : ICommand {
  public void Execute() {
    game.StateGame.Items[game.StateGame.ItemNumber].Use(useType);
  }
}
