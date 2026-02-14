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
      game.Player.UseItem();
    }
  }
}
