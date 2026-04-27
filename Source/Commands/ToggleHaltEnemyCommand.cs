using GameProject.Globals;

namespace GameProject.Commands;

internal class ToggleHaltEnemyCommand : IGPCommand {
  public void Execute() {
    Flags.HaltEnemies = !Flags.HaltEnemies;
  }
}
