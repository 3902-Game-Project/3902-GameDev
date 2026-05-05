using GameProject.Level;

namespace GameProject.Commands;

internal class KillAllLevelEnemiesCommand(ILevelManager levelManager) : IGPCommand {
  public void Execute() {
    levelManager.CurrentLevel.KillAllDamageableEnemies();
  }
}
