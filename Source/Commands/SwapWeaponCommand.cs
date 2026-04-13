using GameProject.Interfaces;
using GameProject.PlayerSpace;
namespace GameProject.Commands;
internal class SwapWeaponCommand(Player player) : ICommand {
  public void Execute() => player.Inventory.SwapActiveWeapon();
}
