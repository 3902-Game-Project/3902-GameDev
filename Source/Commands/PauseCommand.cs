namespace GameProject.Commands;

internal class PauseCommand(Game1 game) : IGPCommand {
  public void Execute() {
    game.ChangeState(game.StatePause);
  }
}
