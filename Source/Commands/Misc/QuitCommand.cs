namespace GameProject.Commands;

internal class QuitCommand(Game1 game) : IGPCommand {
  public void Execute() {
    game.Exit();
  }
}
