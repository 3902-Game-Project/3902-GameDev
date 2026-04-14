namespace GameProject.Commands;

internal class StartGameCommand(Game1 game) : ICommand {
  public void Execute() {
    game.ChangeState(game.StateGame);
  }
}
