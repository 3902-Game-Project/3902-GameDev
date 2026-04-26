using GameProject.Commands;
using GameProject.PlayerSpace;
using GameProject.Source.Misc;

namespace GameProject.Source.Commands;

internal class PlayerUnlimitedItemsCommand(Player player) : IGPCommand {
  public void Execute() {
    CheatCodes.Instance.UnlimitedItems(player);
  }
}
