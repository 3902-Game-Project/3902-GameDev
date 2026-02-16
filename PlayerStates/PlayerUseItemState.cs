using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameProject.Interfaces;

namespace GameProject.PlayerStates {
  public class PlayerUseItemState : IPlayerState {
    private Player player;
    private int timer;

    public PlayerUseItemState(Player player) {
      this.player = player;
      this.timer = 20;
    }

    public void UseItem() {
    }

    public void Update(GameTime gameTime) {
      timer--;
      if (timer <= 0) {
        timer = 20;
      }
    }

    public void Draw(SpriteBatch spriteBatch) {
      Texture2D texture = player.game.GlobalVars.Assets.Textures.MetroTexture;
      Rectangle sourceRect = new Rectangle(0, 0, 22, 30);
      spriteBatch.Draw(texture, player.Position, sourceRect, Color.White);
    }
  }
}
