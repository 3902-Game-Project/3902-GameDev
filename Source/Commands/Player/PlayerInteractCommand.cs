using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerInteractCommand(Player player) : IGPCommand {
  internal void Execute() => player.Interact();
}
