using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GameProject.Interfaces;

namespace GameProject.Commands;
public class SwapWeaponCommand(Player player) : ICommand {
  public void Execute() {
    player.Inventory.SwapActiveWeapon();
  }
}
