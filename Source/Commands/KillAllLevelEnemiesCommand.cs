using GameProject.Managers;

namespace GameProject.Commands;

internal class KillAllLevelEnemiesCommand(ILevelManager levelManager) : IGPCommand {
  public void Execute() {
    levelManager.CurrentLevel.KillAllEnemies();
  }
}
