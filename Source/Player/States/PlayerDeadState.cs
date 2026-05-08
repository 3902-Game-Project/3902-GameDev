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

  internal override void MoveUp() { }
  internal override void MoveDown() { }
  internal override void MoveLeft() { }
  internal override void MoveRight() { }
  internal override void UseItem(UseType useType) { }
  internal override void UseKey(UseType useType) { }
  internal override void TakeDamage(int amount) { }
  internal override void Die() { }
  internal override void Interact() { }

  internal override void Update(double deltaTime) {
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

  internal override void Draw(SpriteBatch spriteBatch) {
    Rectangle sourceRect = SOURCE_RECTS[currentFrame];
    Vector2 origin = new(sourceRect.Width / 2f, sourceRect.Height / 2f);

    spriteBatch.Draw(
      texture: TextureStore.Instance.Player,
      position: Player.Position,
      sourceRectangle: sourceRect,
      color: Color.White,
      rotation: 0f,
      origin: origin,
      scale: Constants.PLAYER_SPRITE_SCALE,
      effects: SpriteEffects.None,
      layerDepth: 0f
    );
  }
}
