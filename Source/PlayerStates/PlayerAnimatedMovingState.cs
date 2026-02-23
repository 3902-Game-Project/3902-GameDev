using System.Collections.Generic;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerStates;

public class PlayerAnimatedMovingState(Player player) : IPlayerState {
  private List<Rectangle> moveLeftFrames = [
        new(1520, 419, 188, 327),
        new(1860, 419, 188, 327)
    ];

  private List<Rectangle> moveRightFrames = [
      new(2470, 51, 191, 326),
      new(2152, 51, 191, 326)
  ];

  private int currentFrame = 0;
  private double timer = 0;
  private double frameInterval = 0.2;

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

  public void UseItem() {
    player.State = player.UseItemState;
  }

  public void Draw(SpriteBatch spriteBatch) {
    Rectangle sourceRect;

    if (player.Direction == FacingDirection.Right) {
      sourceRect = moveRightFrames[currentFrame];
    } else {
      sourceRect = moveLeftFrames[currentFrame];
    }

    Vector2 origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);

    spriteBatch.Draw(
        player.Texture,
        player.Position,
        sourceRect,
        Color.White,
        0f,
        origin,
        0.2f,
        SpriteEffects.None,
        0f
    );
  }
}
