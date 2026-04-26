namespace GameProject.Commands;

internal class ReturnToMenuAndResetCommand(Game1 game) : IGPCommand {
  public void Execute() {
    game.ChangeState(game.StateMenu);
    game.ResetGameState();
  }
}
