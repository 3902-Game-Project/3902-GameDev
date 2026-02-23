using GameProject.Interfaces;

namespace GameProject.Commands;

public class PreviousEnemyCommand : ICommand {
  private Game1 game;

  public PreviousEnemyCommand(Game1 game) {
    this.game = game;
  }

  public void Execute() {
    game.StateGame.EnemyNumber--;

    if (game.StateGame.EnemyNumber < 0) {
      game.StateGame.EnemyNumber = game.StateGame.Enemies.Count - 1;
    }
  }
}
