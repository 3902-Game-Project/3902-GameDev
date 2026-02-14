using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameProject.Interfaces;

namespace GameProject {
  public class Player {
    private Game1 game;
    public IPlayerState State { get; set; } // Current state
    public Vector2 Position { get; set; }

    public IItem CurrentItem { get; set; }

    public Player(Game1 game) {
      this.game = game;
      this.Position = new Vector2(400, 300);

      // For now, attack state is default
      this.State = new PlayerUseItemState(this);
    }

    public void UseItem() {
      State.UseItem();
    }

    public void Update(GameTime gameTime) {
      State.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch) {
      State.Draw(spriteBatch);
    }
  }
}
