using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GameProject.Interfaces;

namespace GameProject.Commands {
  public class PlayerUseItemCommand : ICommand {
    private Game1 game;

    public PlayerUseItemCommand(Game1 game) {
      this.game = game;
    }

    public void Execute() {
      // Because only a player would be calling UseItem, refactor to just use a Player object and remove need for game.Player
      game.Player.UseItem();
    }
  }
}
