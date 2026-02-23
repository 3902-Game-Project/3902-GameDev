using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerStates;

public class PlayerUseItemState(Player player) : IPlayerState {
  private int timer = 20;

  public void MoveUp() { }
  public void MoveDown() { }
  public void MoveLeft() { }
  public void MoveRight() { }
  public void UseItem() { }

  public void Update(GameTime gameTime) {
    timer--;
    if (timer <= 0) {
      timer = 20;
      player.State = player.StaticState;
    }
  }

  public void Draw(SpriteBatch spriteBatch) {
    Texture2D texture = player.game.Assets.Textures.MetroTexture;
    Rectangle sourceRect = new(0, 0, 22, 30);
    spriteBatch.Draw(texture, player.Position, sourceRect, Color.White);
  }
}
