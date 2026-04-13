using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerSpace.States;

internal class PlayerStaticState(Player player) : IPlayerState {
  // Same rectangles as moving state except that left state set to same as right state (but flipped) for consistency
  private Rectangle SpriteRight = new(773, 56, 171, 323);
  private Rectangle SpriteLeft = new(773, 56, 171, 323);
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
    if (player.Inventory.ActiveItem != null) {
      player.Inventory.ActiveItem.Use(useType);
      if (player.Inventory.ActiveItem.Category == Enums.ItemCategory.Consumable) {
        player.State = player.UseItemState;
      }
    }
  }

  public void Die() {
    player.State = player.DeadState;
  }

  public void Draw(SpriteBatch spriteBatch) {
    Rectangle sourceRect;
    Vector2 origin;

    // Check direction even when standing still
    SpriteEffects flipStatus;

    if (player.Direction == FacingDirection.Right) {
      sourceRect = SpriteRight;
      flipStatus = SpriteEffects.None;
    } else if (player.Direction == FacingDirection.Left) {
      sourceRect = SpriteLeft;
      flipStatus = SpriteEffects.FlipHorizontally;
    } else if (player.Direction == FacingDirection.Up) {
      sourceRect = SpriteUp;
      flipStatus = SpriteEffects.None;
    } else {
      sourceRect = SpriteDown;
      flipStatus = SpriteEffects.None;
    }

    origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);

    spriteBatch.Draw(
      texture: player.Texture,
      position: player.Position,
      sourceRectangle: sourceRect,
      color: Color.White,
      rotation: 0f,
      origin: origin,
      scale: 0.15f,
      effects: flipStatus,
      layerDepth: 0f
    );
  }
}

