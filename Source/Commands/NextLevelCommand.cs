using GameProject.Interfaces;

namespace GameProject.Commands;

internal class NextLevelCommand(Game1 game) : ICommand {
  public void Execute() {
    game.StateGame.LevelManager.NextLevel();
  }
}
