using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerStates;

public class PlayerDeadState(Player player) : IPlayerState {

  private Rectangle deadSprite = new(1470, 1060, 304, 97);
  public void MoveUp() { }
  public void MoveDown() { }
  public void MoveLeft() { }
  public void MoveRight() { }
  public void UseItem(UseType useType) { }
  public void Die() { }

  public void Update(GameTime gameTime) {
    player.Velocity = Vector2.Zero;
  }

  public void Draw(SpriteBatch spriteBatch) {
    Vector2 origin = new(deadSprite.Width / 2, deadSprite.Height / 2);

    spriteBatch.Draw(
      player.Texture,
      player.Position,
      deadSprite,
      Color.White,
      0f,
      origin,
      0.15f,
      SpriteEffects.None,
      0f
    );
  }
}
