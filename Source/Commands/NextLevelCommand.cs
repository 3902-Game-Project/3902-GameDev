using GameProject.Managers;

namespace GameProject.Commands;

internal class NextLevelCommand(ILevelManager levelManager) : ICommand {
  public void Execute() {
    levelManager.NextLevel();
  }
}
