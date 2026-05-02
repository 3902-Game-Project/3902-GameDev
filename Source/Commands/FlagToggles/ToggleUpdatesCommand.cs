using GameProject.Globals;

namespace GameProject.Commands;

internal class ToggleUpdatesCommand : IGPCommand {
  public void Execute() {
    Flags.HaltAllUpdates = !Flags.HaltAllUpdates;
  }
}
