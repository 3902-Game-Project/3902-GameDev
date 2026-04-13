using GameProject.Interfaces;
using GameProject.PlayerSpace;
namespace GameProject.Commands;
internal class PlayerInteractCommand(Player player) : ICommand {
  public void Execute() => player.Interact();
}
