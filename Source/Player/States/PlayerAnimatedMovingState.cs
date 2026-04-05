using System.Collections.Generic;
using GameProject.Interfaces;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerStates;

public class PlayerAnimatedMovingState(Player player) : IPlayerState {
  private readonly List<Rectangle> moveLeftFrames = [
        new(1531, 420, 171, 323),
        new(1854, 427, 171, 323)
    ];

  private readonly List<Rectangle> moveRightFrames = [
      new(2161, 52, 171, 323),
      new(2481, 54, 171, 323)
  ];

  private int currentFrame = 0;
  private double timer = 0;
  private readonly double frameInterval = 0.2;

  public void MoveUp() {
    player.Velocity = new Vector2(player.Velocity.X, -player.Speed);
  }
  public void MoveDown() {
    player.Velocity = new Vector2(player.Velocity.X, player.Speed);
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
    if (player.Velocity == Vector2.Zero) {
      player.State = player.StaticState;
      currentFrame = 0;
      return;
    }
    timer += gameTime.ElapsedGameTime.TotalSeconds;
    if (timer > frameInterval) {
      currentFrame++;
      if (currentFrame >= moveLeftFrames.Count) {
        currentFrame = 0;
      }
      timer = 0;
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

    if (player.Direction == FacingDirection.Right) {
      sourceRect = moveRightFrames[currentFrame];
    } else {
      sourceRect = moveLeftFrames[currentFrame];
    }

    Vector2 origin = new(sourceRect.Width / 2, sourceRect.Height / 2);

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
