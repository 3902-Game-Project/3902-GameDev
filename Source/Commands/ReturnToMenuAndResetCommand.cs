using GameProject.GlobalInterfaces;

namespace GameProject.Commands;

internal class ReturnToMenuAndResetCommand(Game1 game) : ICommand {
  public void Execute() {
    game.ChangeState(game.StateMenu);
    game.ResetGameState();
  }
}
