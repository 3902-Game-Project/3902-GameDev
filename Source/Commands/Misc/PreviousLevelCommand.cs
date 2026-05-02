using GameProject.Managers;

namespace GameProject.Commands;

internal class PreviousLevelCommand(ILevelManager levelManager) : IGPCommand {
  public void Execute() {
    levelManager.PreviousLevel();
  }
}
