namespace GameProject.Commands;

internal class ReturnToGameNoFadeCommand(Game1 game) : ICommand {
  public void Execute() {
    game.ChangeStateWithoutFading(game.StateGame);
  }
}
