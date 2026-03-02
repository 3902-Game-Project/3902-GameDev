using GameProject.Interfaces;

namespace GameProject.Commands;

internal class PreviousLevelCommand(Game1 game) : ICommand {
  public void Execute() {
    game.StateGame.LevelManager.PreviousLevel();
  }
}
