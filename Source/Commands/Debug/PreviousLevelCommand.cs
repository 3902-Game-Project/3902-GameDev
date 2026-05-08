using GameProject.Level;

namespace GameProject.Commands;

internal class PreviousLevelCommand(ILevelManager levelManager) : IGPCommand {
  internal void Execute() {
    levelManager.PreviousLevel();
  }
}
