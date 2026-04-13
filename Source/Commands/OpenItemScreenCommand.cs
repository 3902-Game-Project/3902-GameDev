using GameProject.Interfaces;

namespace GameProject.Commands;

public class OpenItemScreenCommand(Game1 game) : ICommand {
  public void Execute() {
    // TODO Change this funciton to ChangeStateWithoutFading
    game.ChangeState(game.StateItemScreen);
  }
}
