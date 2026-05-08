using GameProject.Globals;

namespace GameProject.Commands;

internal class ToggleSlowModeCommand() : IGPCommand {
  internal void Execute() {
    Flags.SlowMode = !Flags.SlowMode;
  }
}
