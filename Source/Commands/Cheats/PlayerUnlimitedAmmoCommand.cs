using GameProject.Commands;
using GameProject.PlayerSpace;
using GameProject.Source.Misc;

namespace GameProject.Source.Commands;

internal class PlayerUnlimitedAmmoCommand(Player player) : IGPCommand {
  internal void Execute() {
    CheatCodes.UnlimitedAmmo(player);
  }
}
