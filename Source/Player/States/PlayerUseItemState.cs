using GameProject.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerSpace.States;

internal class PlayerUseItemState(Player player) : APlayerState {
  private int timer = 20;

  private Rectangle SpriteRight = new(773, 56, 171, 323);
  private Rectangle SpriteLeft = new(1531, 420, 171, 323);
  private Rectangle SpriteUp = new(453, 425, 161, 322);
  private Rectangle SpriteDown = new(455, 58, 161, 318);
  public override void MoveUp() { }
  public override void MoveDown() { }
  public override void MoveLeft() { }
  public override void MoveRight() { }
  public override void UseItem(UseType useType) { }
  public override void UseKey(UseType useType) { }

  public override void Die() {
    player.State = player.DeadState;
  }

  public override void Update(GameTime gameTime) {
    timer--;
    if (timer <= 0) {
      timer = 20;
      player.State = player.StaticState;
    }
  }

  public override void Draw(SpriteBatch spriteBatch) {
    Rectangle sourceRect;

    if (player.Direction == FacingDirection.Right) {
      sourceRect = SpriteRight;
    } else if (player.Direction == FacingDirection.Left) {
      sourceRect = SpriteLeft;
    } else if (player.Direction == FacingDirection.Up) {
      sourceRect = SpriteUp;
    } else {
      sourceRect = SpriteDown;
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
