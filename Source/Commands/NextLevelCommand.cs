using GameProject.Managers;

namespace GameProject.Commands;

internal class NextLevelCommand(ILevelManager levelManager) : IGPCommand {
  public void Execute() {
    levelManager.NextLevel();
  }
}
