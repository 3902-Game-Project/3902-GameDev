using GameProject.Interfaces;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerStates;

internal class PlayerUseItemState(Player player) : IPlayerState {
  private int timer = 20;

  private Rectangle SpriteRight = new(773, 56, 171, 323);
  private Rectangle SpriteLeft = new(1531, 420, 171, 323);

  public void MoveUp() { }
  public void MoveDown() { }
  public void MoveLeft() { }
  public void MoveRight() { }
  public void UseItem(UseType useType) { }

  public void Die() {
    player.State = player.DeadState;
  }

  public void Update(GameTime gameTime) {
    timer--;
    if (timer <= 0) {
      timer = 20;
      player.State = player.StaticState;
    }
  }

  public void Draw(SpriteBatch spriteBatch) {
    Rectangle sourceRect;

    if (player.Direction == FacingDirection.Right) {
      sourceRect = SpriteRight;
    } else {
      sourceRect = SpriteLeft;
    }

    Vector2 origin = new(sourceRect.Width / 2, sourceRect.Height / 2);

    spriteBatch.Draw(
      player.Texture,
      player.Position,
      sourceRect,
      Color.White,
      0f,
      origin,
      0.15f, // Keeps the scale consistent with other states
      SpriteEffects.None,
      0f
    );
  }
}
