namespace GameProject.Commands;

internal class WinScreenCommand(Game1 game) : IGPCommand {
  public void Execute() {
    game.ChangeState(game.StateWin);
  }
}
