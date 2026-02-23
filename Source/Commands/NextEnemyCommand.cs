using GameProject.Interfaces;

namespace GameProject.Commands;

public class NextEnemyCommand : ICommand {
  private Game1 game;

  public NextEnemyCommand(Game1 game) {
    this.game = game;
  }

  public void Execute() {
    game.StateGame.EnemyNumber++;

    if (game.StateGame.EnemyNumber >= game.StateGame.Enemies.Count) {
      game.StateGame.EnemyNumber = 0;
    }
  }
}
