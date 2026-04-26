namespace GameProject.Commands;

internal class ReturnToGameNoFadeCommand(Game1 game) : IGPCommand {
  public void Execute() {
    game.ChangeStateWithoutFading(game.StateGame);
  }
}
