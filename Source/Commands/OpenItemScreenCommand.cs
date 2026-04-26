namespace GameProject.Commands;

internal class OpenItemScreenCommand(Game1 game) : IGPCommand {
  public void Execute() {
    game.ChangeStateWithoutFading(game.StateItemScreen);
  }
}
