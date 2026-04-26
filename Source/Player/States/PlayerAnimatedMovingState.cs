using System.Collections.Generic;
using GameProject.Controllers;
using GameProject.Globals;
using GameProject.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerSpace.States;

internal class PlayerAnimatedMovingState(Player player) : APlayerState(player) {
  private readonly List<Rectangle> moveLeftFrames = [
    new(1531, 420, 171, 323),
    new(1854, 427, 171, 323)
  ];

  private readonly List<Rectangle> moveRightFrames = [
    new(2161, 52, 171, 323),
    new(2481, 54, 171, 323),
  ];

  // TODO: need to update below 2 later
  private readonly List<Rectangle> moveUpFrames = [
    new (130, 813, 159, 335),
    new (453, 813, 161, 335),
    new (1083, 813, 164, 335),
  ];

  private readonly List<Rectangle> moveDownFrames = [
    new (448, 1181, 177, 313),
    new (1256, 1181, 175, 320),
    new (991, 1181, 176, 313),
    new (727, 1181, 178, 320),
  ];

  private int currentFrame = 0;
  private double timer = 0;
  private readonly double frameInterval = 0.2;

  public override void MoveUp() {
    Player.Velocity = new Vector2(Player.Velocity.X, -Player.Speed);
  }
  public override void MoveDown() {
    Player.Velocity = new Vector2(Player.Velocity.X, Player.Speed);
  }
  public override void MoveLeft() {
    Player.Velocity = new Vector2(-Player.Speed, Player.Velocity.Y);
  }
  public override void MoveRight() {
    Player.Velocity = new Vector2(Player.Speed, Player.Velocity.Y);
  }

  public override void UseItem(UseType useType) {
    if (Player.Inventory.ActiveItem != null) {
      Player.Inventory.ActiveItem.Use(useType);
      if (Player.Inventory.ActiveItem.Category == ItemCategory.Consumable) {
        Player.State = Player.UseItemState;
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
        Player.State = Player.UseItemState;
      }
      Player.Inventory.RemoveGeneralItem(keyToUse);
    }
  }

  public override void Update(double deltaTime) {
    if (Player.Velocity == Vector2.Zero) {
      Player.State = Player.StaticState;
      currentFrame = 0;
      return;
    }

    timer += deltaTime;
    if (timer > frameInterval) {
      currentFrame++;
      if (currentFrame >= 2) {
        currentFrame = 0;
      }
      timer -= frameInterval;
    }
  }

  public override void Draw(SpriteBatch spriteBatch) {
    List<Rectangle> activeFrames;

    if (Player.Direction == FacingDirection.Right) {
      activeFrames = moveRightFrames;
    } else if (Player.Direction == FacingDirection.Left) {
      activeFrames = moveLeftFrames;
    } else if (Player.Direction == FacingDirection.Up) {
      activeFrames = moveUpFrames;
    } else {
      activeFrames = moveDownFrames;
    }
    int frameIndex = currentFrame % activeFrames.Count;
    Rectangle sourceRect = activeFrames[frameIndex];
    Vector2 origin = new(sourceRect.Width / 2, sourceRect.Height / 2);

    spriteBatch.Draw(
      texture: TextureStore.Instance.Player,
      position: Player.Position,
      sourceRectangle: sourceRect,
      color: Player.CurrentTintColor,
      rotation: 0f,
      origin: origin,
      scale: 0.15f,
      effects: SpriteEffects.None,
      layerDepth: 0f
    );
  }
}
