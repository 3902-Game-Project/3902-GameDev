using GameProject.Interfaces;
namespace GameProject.Commands;
public class SwapWeaponCommand(Player player) : ICommand {
  public void Execute() => player.Inventory.SwapActiveWeapon();
}
