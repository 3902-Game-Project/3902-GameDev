using GameProject.Globals;

namespace GameProject.Commands;

internal class ToggleVignetteCommand : IGPCommand {
  internal void Execute() {
    Flags.Vignette = !Flags.Vignette;
  }
}
