using System;
using System.Collections.Generic;
using GameProject.Controllers;
using GameProject.Globals;
using GameProject.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.PlayerSpace.States;

internal class PlayerDeadState(Player player, Action onLoss) : APlayerState(player) {
  private static readonly List<Rectangle> SOURCE_RECTS = [
    new(2116, 1032, 282, 129),
    new(1807, 1034, 277, 127),
    new(1473, 1053, 298, 108)
  ];

  private int currentFrame = 0;
  private double animationTimer = 0;
  private readonly double frameInterval = 0.5;
  private readonly GPTimer deadTimer = new();

  public override void MoveUp() { }
  public override void MoveDown() { }
  public override void MoveLeft() { }
  public override void MoveRight() { }
  public override void UseItem(UseType useType) { }
  public override void UseKey(UseType useType) { }
  public override void TakeDamage(int amount) { }
  public override void Die() { }

  public override void Update(double deltaTime) {
    base.Update(deltaTime);

    Player.Velocity = Vector2.Zero;

    if (currentFrame < SOURCE_RECTS.Count - 1) {
      animationTimer += deltaTime;
      if (animationTimer > frameInterval) {
        currentFrame++;
        animationTimer -= frameInterval;
      }
    }

    deadTimer.Update(deltaTime);
    if (deadTimer.Time >= Constants.LOSS_SCREEN_WAIT) {
      onLoss?.Invoke();
    }
  }

  public override void Draw(SpriteBatch spriteBatch) {
    Rectangle sourceRect = SOURCE_RECTS[currentFrame];
    Vector2 origin = new(sourceRect.Width / 2f, sourceRect.Height / 2f);

    spriteBatch.Draw(
      texture: TextureStore.Instance.Player,
      position: Player.Position,
      sourceRectangle: sourceRect,
      color: Color.White,
      rotation: 0f,
      origin: origin,
      scale: 0.15f,
      effects: SpriteEffects.None,
      layerDepth: 0f
    );
  }
}
