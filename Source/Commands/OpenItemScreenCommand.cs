namespace GameProject.Commands;

internal class OpenItemScreenCommand(Game1 game) : ICommand {
  public void Execute() {
    game.ChangeStateWithoutFading(game.StateItemScreen);
  }
}
