using GameProject.GlobalInterfaces;

namespace GameProject.Commands;

internal class ReturnToGameCommand(Game1 game) : ICommand {
  public void Execute() {
    game.ChangeState(game.StateGame);
  }
}
