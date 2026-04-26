namespace GameProject.Commands;

internal class ReturnToGameCommand(Game1 game) : IGPCommand {
  public void Execute() {
    game.ChangeState(game.StateGame);
  }
}
