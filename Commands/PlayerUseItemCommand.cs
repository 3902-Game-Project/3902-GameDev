using GameProject.Interfaces;

namespace GameProject.Commands;

public class PlayerUseItemCommand(Game1 game) : ICommand {
  public void Execute() {
    // Because only a player would be calling UseItem, refactor to just use a Player object and remove need for game.Player
    game.Player.UseItem();
  }
}
