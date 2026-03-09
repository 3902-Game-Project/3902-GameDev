using GameProject.Enums;
using GameProject.Interfaces;

namespace GameProject.Commands {
  public class SwitchItemCommand : ICommand {
    private Game1 game;
    private ItemCategory category;

    public SwitchItemCommand(Game1 game, ItemCategory category) {
      this.game = game;
      this.category = category;
    }

    public void Execute() {
      game.StateGame.Player.Inventory.SwitchActiveItem(category);
    }
  }
}
