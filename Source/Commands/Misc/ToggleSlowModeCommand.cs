using GameProject.Globals;

namespace GameProject.Commands;

internal class ToggleSlowModeCommand() : IGPCommand {
  public void Execute() {
    Flags.SlowMode = !Flags.SlowMode;
  }
}
