using GameProject.Level;

namespace GameProject.Commands;

internal class KillAllLevelEnemiesCommand(ILevelManager levelManager) : IGPCommand {
  internal void Execute() {
    levelManager.CurrentLevel.KillAllDamageableEnemies();
  }
}
