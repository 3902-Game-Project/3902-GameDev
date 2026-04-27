using GameProject.Globals;

namespace GameProject.Commands;

internal class ToggleVignetteCommand : IGPCommand {
  public void Execute() {
    Flags.Vignette = !Flags.Vignette;
  }
}
