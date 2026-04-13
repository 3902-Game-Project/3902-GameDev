using GameProject.Interfaces;

namespace GameProject.Commands;

public class OpenItemScreenCommand(Game1 game) : ICommand {
  public void Execute() {
    // TODO Change this function to ChangeStateWithoutFading
    game.ChangeState(game.StateItemScreen);
  }
}
