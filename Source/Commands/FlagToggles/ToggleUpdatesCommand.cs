using GameProject.Globals;

namespace GameProject.Commands;

internal class ToggleUpdatesCommand : IGPCommand {
  internal void Execute() {
    Flags.HaltAllUpdates = !Flags.HaltAllUpdates;
  }
}
