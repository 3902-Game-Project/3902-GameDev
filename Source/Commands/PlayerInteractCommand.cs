using GameProject.Interfaces;
namespace GameProject.Commands;
public class PlayerInteractCommand(Player player) : ICommand {
  public void Execute() => player.Interact();
}
