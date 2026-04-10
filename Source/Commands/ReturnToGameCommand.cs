using GameProject.Interfaces;

namespace GameProject.Commands;

public class ReturnToGameCommand(Game1 game) : ICommand {
  public void Execute() {
    game.ChangeState(game.StateGame);
  }
}
