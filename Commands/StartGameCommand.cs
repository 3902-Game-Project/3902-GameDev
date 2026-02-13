using GameProject.Interfaces;

namespace GameProject.Commands;

public class StartGameCommand(Game1 game) : ICommand {
  public void Execute() {
    game.ChangeState(game.StateGame);
  }
}
