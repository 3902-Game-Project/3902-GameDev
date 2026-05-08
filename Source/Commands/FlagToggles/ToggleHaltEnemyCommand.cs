using GameProject.Globals;

namespace GameProject.Commands;

internal class ToggleHaltEnemyCommand : IGPCommand {
  internal void Execute() {
    Flags.HaltEnemies = !Flags.HaltEnemies;
  }
}
