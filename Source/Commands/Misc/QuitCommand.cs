namespace GameProject.Commands;

internal class QuitCommand(Game1 game) : IGPCommand {
  internal void Execute() {
    game.Exit();
  }
}
