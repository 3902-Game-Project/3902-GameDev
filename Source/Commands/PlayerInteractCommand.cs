using GameProject.PlayerSpace;

namespace GameProject.Commands;

internal class PlayerInteractCommand(Player player) : IGPCommand {
  public void Execute() => player.Interact();
}
