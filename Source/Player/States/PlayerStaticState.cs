using GameProject.Interfaces;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerStates;

public class PlayerStaticState(Player player) : IPlayerState {
  // Same rectangles as moving state
  private Rectangle SpriteRight = new(773, 56, 171, 323);
  private Rectangle SpriteLeft = new(1531, 420, 171, 323);
  private Rectangle SpriteUp = new(453, 425, 161, 322); 
  private Rectangle SpriteDown = new(455, 58, 161, 318);

  public void MoveUp() {
    player.Velocity = new Vector2(player.Velocity.X, -player.Speed);
    player.Direction = FacingDirection.Up;
  }
  public void MoveDown() {
    player.Velocity = new Vector2(player.Velocity.X, player.Speed);
    player.Direction = FacingDirection.Down;
  }
  public void MoveLeft() {
    player.Velocity = new Vector2(-player.Speed, player.Velocity.Y);
    player.Direction = FacingDirection.Left;
  }
  public void MoveRight() {
    player.Velocity = new Vector2(player.Speed, player.Velocity.Y);
    player.Direction = FacingDirection.Right;
  }

  public void Update(GameTime gameTime) {
    // If we start moving, switch state
    if (player.Velocity != Vector2.Zero) {
      player.State = player.MovingState;
    }
  }

  public void UseItem(UseType useType) {
    player.State = player.UseItemState;
  }

  public void Die() {
    player.State = player.DeadState;
  }

  public void Draw(SpriteBatch spriteBatch) {

    Rectangle sourceRect;
    Vector2 origin;

    // Check direction even when standing still
    if (player.Direction == FacingDirection.Right) {
      sourceRect = SpriteRight;
    } else if (player.Direction == FacingDirection.Left) {
      sourceRect = SpriteLeft;
    } else if (player.Direction == FacingDirection.Up) {
      sourceRect = SpriteUp;
    } else {
      sourceRect = SpriteDown;
    }

    origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);

    spriteBatch.Draw(
      player.Texture,
      player.Position,
      sourceRect,
      Color.White,
      0f,
      origin,
      0.15f,
      SpriteEffects.None,
      0f
    );
  }
}

