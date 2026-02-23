using GameProject.Interfaces;

namespace GameProject.Commands;

public class DamageEnemyCommand : ICommand {
  private Game1 game;

  public DamageEnemyCommand(Game1 game) {
    this.game = game;
  }

  public void Execute() {
    if (game.StateGame.Enemies != null && game.StateGame.Enemies.Count > 0) {
      game.StateGame.Enemies[game.StateGame.EnemyNumber].TakeDamage();
    }
  }
}
