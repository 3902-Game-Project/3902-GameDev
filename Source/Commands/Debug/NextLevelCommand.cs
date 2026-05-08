using GameProject.Level;

namespace GameProject.Commands;

internal class NextLevelCommand(ILevelManager levelManager) : IGPCommand {
  internal void Execute() {
    levelManager.NextLevel();
  }
}
