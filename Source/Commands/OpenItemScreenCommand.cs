using GameProject.Interfaces;

namespace GameProject.Commands;

public class OpenItemScreenCommand(Game1 game) : ICommand {
  public void Execute() {
    game.ChangeStateWithoutFading(game.StateItemScreen);
  }
}
