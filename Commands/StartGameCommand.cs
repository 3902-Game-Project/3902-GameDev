using GameProject.Interfaces;

namespace GameProject.Commands;

public class StartGameCommand : ICommand {
  private Game1 game;

  public StartGameCommand(Game1 game) {
    this.game = game;
  }

  public void Execute() {
    game.ChangeState(game.StateGame);
  }
}
