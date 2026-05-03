using GameProject.Controllers;
using GameProject.Globals;
using GameProject.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerSpace.States;

internal class PlayerStaticState(Player player) : APlayerState(player) {
  // Same rectangles as moving state except that left state set to same as right state (but flipped) for consistency
  private Rectangle SpriteRight = new(773, 56, 171, 323);
  private Rectangle SpriteLeft = new(773, 56, 171, 323);
  private Rectangle SpriteUp = new(453, 425, 161, 322);
  private Rectangle SpriteDown = new(455, 58, 161, 318);

  public override void MoveUp() {
    Player.Velocity = new Vector2(Player.Velocity.X, -Player.Speed);
    Player.Direction = FacingDirection.Up;
  }

  public override void MoveDown() {
    Player.Velocity = new Vector2(Player.Velocity.X, Player.Speed);
    Player.Direction = FacingDirection.Down;
  }

  public override void MoveLeft() {
    Player.Velocity = new Vector2(-Player.Speed, Player.Velocity.Y);
    Player.Direction = FacingDirection.Left;
  }

  public override void MoveRight() {
    Player.Velocity = new Vector2(Player.Speed, Player.Velocity.Y);
    Player.Direction = FacingDirection.Right;
  }

  public override void Update(double deltaTime) {
    // If we start moving, switch state
    if (Player.Velocity != Vector2.Zero) {
      Player.ChangeState(Player.MovingState);
    }
  }

  public override void UseItem(UseType useType) {
    if (Player.Inventory.ActiveItem != null) {
      Player.Inventory.ActiveItem.Use(useType);
      if (Player.Inventory.ActiveItem.Category == ItemCategory.Consumable) {
        Player.ChangeState(Player.UseItemState);
      }
    }
  }

  public override void UseKey(UseType useType) {
    IItem keyToUse = null;
    foreach (var item in Player.Inventory.GeneralItems) {
      if (item is KeyItem) {
        keyToUse = item;
        break;
      }
    }
    if (keyToUse != null) {
      keyToUse.Use(useType);

      if (Player.Inventory.ActiveItem != null && Player.Inventory.ActiveItem.Category == ItemCategory.Consumable) {
        Player.ChangeState(Player.UseItemState);
      }

      Player.Inventory.RemoveGeneralItem(keyToUse);
    }
  }

  public override void Draw(SpriteBatch spriteBatch) {
    Rectangle sourceRect;
    Vector2 origin;

    // Check direction even when standing still
    SpriteEffects flipStatus;

    if (Player.Direction == FacingDirection.Right) {
      sourceRect = SpriteRight;
      flipStatus = SpriteEffects.None;
    } else if (Player.Direction == FacingDirection.Left) {
      sourceRect = SpriteLeft;
      flipStatus = SpriteEffects.FlipHorizontally;
    } else if (Player.Direction == FacingDirection.Up) {
      sourceRect = SpriteUp;
      flipStatus = SpriteEffects.None;
    } else {
      sourceRect = SpriteDown;
      flipStatus = SpriteEffects.None;
    }

    origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);

    spriteBatch.Draw(
      texture: TextureStore.Instance.Player,
      position: Player.Position,
      sourceRectangle: sourceRect,
      color: Player.CurrentTintColor,
      rotation: 0f,
      origin: origin,
      scale: 0.15f,
      effects: flipStatus,
      layerDepth: 0f
    );
  }
}

