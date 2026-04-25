using GameProject.Commands;
using GameProject.Controllers;
using GameProject.PlayerSpace;
using GameProject.Source.Misc;

namespace GameProject.Source.Commands;

internal class PlayerUnlimitedHealthCommand(Player player) : IGPCommand {
  public void Execute() {
    CheatCodes.Instance.UnlimitedHealth(player);
  }
}
