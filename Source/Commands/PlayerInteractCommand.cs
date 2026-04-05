using GameProject.Interfaces;
using GameProject.PlayerSpace;
namespace GameProject.Commands;
public class PlayerInteractCommand(Player player) : ICommand {
  public void Execute() => player.Interact();
}
