using System.Collections.Generic;
using GameProject.Controllers;
using GameProject.Globals;
using GameProject.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerSpace.States;

internal class PlayerMovingState(Player player) : APlayerState(player) {
  private static readonly double FRAME_INTERVAL = 0.2;

  private static readonly List<Rectangle> MOVE_LEFT_FRAMES = [
    new(1531, 420, 171, 323),
    new(1854, 427, 171, 323)
  ];

  private static readonly List<Rectangle> MOVE_RIGHT_FRAMES = [
    new(2161, 52, 171, 323),
    new(2481, 54, 171, 323),
  ];

  // TODO: need to update below 2 later
  private static readonly List<Rectangle> MOVE_UP_FRAMES = [
    new (130, 813, 159, 335),
    new (453, 813, 161, 335),
    new (1083, 813, 164, 335),
  ];

  private static readonly List<Rectangle> MOVE_DOWN_FRAMES = [
    new (448, 1181, 177, 313),
    new (1256, 1181, 175, 320),
    new (991, 1181, 176, 313),
    new (727, 1181, 178, 320),
  ];

  private int currentFrame = 0;
  private double timer = 0;

  private List<Rectangle> ActiveFrames {
    get {
      if (Player.Direction == FacingDirection.Right) {
        return MOVE_RIGHT_FRAMES;
      } else if (Player.Direction == FacingDirection.Left) {
        return MOVE_LEFT_FRAMES;
      } else if (Player.Direction == FacingDirection.Up) {
        return MOVE_UP_FRAMES;
      } else {
        return MOVE_DOWN_FRAMES;
      }
    }
  }

  public override void MoveUp() {
    Player.Velocity = new Vector2(Player.Velocity.X, -Player.PLAYER_SPEED);
  }

  public override void MoveDown() {
    Player.Velocity = new Vector2(Player.Velocity.X, Player.PLAYER_SPEED);
  }

  public override void MoveLeft() {
    Player.Velocity = new Vector2(-Player.PLAYER_SPEED, Player.Velocity.Y);
  }

  public override void MoveRight() {
    Player.Velocity = new Vector2(Player.PLAYER_SPEED, Player.Velocity.Y);
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

  public override void Update(double deltaTime) {
    base.Update(deltaTime);

    if (Player.Velocity == Vector2.Zero) {
      Player.ChangeState(Player.StaticState);
      currentFrame = 0;
      return;
    }

    timer += deltaTime;
    if (timer > FRAME_INTERVAL) {
      currentFrame++;
      if (currentFrame >= ActiveFrames.Count) {
        currentFrame = 0;
      }
      timer -= FRAME_INTERVAL;
    }
  }

  public override void Draw(SpriteBatch spriteBatch) {
    var activeFrames = ActiveFrames;

    int frameIndex = currentFrame % activeFrames.Count;
    Rectangle sourceRect = activeFrames[frameIndex];
    Vector2 origin = new(sourceRect.Width / 2, sourceRect.Height / 2);

    spriteBatch.Draw(
      texture: TextureStore.Instance.Player,
      position: Player.Position,
      sourceRectangle: sourceRect,
      color: CurrentTintColor,
      rotation: 0f,
      origin: origin,
      scale: 0.15f,
      effects: SpriteEffects.None,
      layerDepth: 0f
    );
  }
}
