using System.Collections.Generic;
using GameProject.Controllers;
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

  //need to update below 2 later
  private readonly List<Rectangle> moveUpFrames = [
    new(453, 425, 161, 322),
    new(453, 425, 161, 322),
  ];

  private readonly List<Rectangle> moveDownFrames = [
    new(455, 58, 161, 318),
    new(455, 58, 161, 318),
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
    if (Player.Inventory.Keys.Count > 0) {
      Player.Inventory.Keys[0].Use(useType);
      if (Player.Inventory.ActiveItem.Category == ItemCategory.Consumable) {
        Player.State = Player.UseItemState;
      }
      Player.Inventory.Keys.RemoveAt(0);
    }
  }

  public override void Update(GameTime gameTime) {
    if (Player.Velocity == Vector2.Zero) {
      Player.State = Player.StaticState;
      currentFrame = 0;
      return;
    }
    timer += gameTime.ElapsedGameTime.TotalSeconds;
    if (timer > frameInterval) {
      currentFrame++;
      if (currentFrame >= 2) {
        currentFrame = 0;
      }
      timer = 0;
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
      Player.Texture,
      Player.Position,
      sourceRect,
      player.CurrentTintColor,
      0f,
      origin,
      0.15f,
      SpriteEffects.None,
      0f
    );
  }
}
