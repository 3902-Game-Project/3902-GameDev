using System;
using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Interfaces;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class SlattedDoorBlock(Texture2D SlattedDoorTexture, Vector2 xyPos, LockableDoorBlockState state, string pairedLevelName, ILevelManager levelManager) : BaseBlock(xyPos) {
  private int currentFrame = 0;
  private readonly List<Rectangle> sourceRects = [
      new(192, 128, 64, 64),
      new(320, 128, 64, 64)
    ];
  private Boolean rotated = false;
  public float Rotation { get; private set; } = 0.0f;
  public string PairedLevelName { get; private set; } = pairedLevelName;
  public LockableDoorBlockState State { get; private set; } = state;

  public void Rotate() {
    float x = Position.X, y = Position.Y;
    if (x < 64) {
      Rotation = MathHelper.ToRadians(270);
      y += 64;
    } else if (x >= 896 && y >= 64) {
      Rotation = MathHelper.ToRadians(90);
      x += 64;
    } else if (y >= 512 && x >= 64) {
      Rotation = MathHelper.ToRadians(180);
      x += 64;
      y += 64;
    }
    Position = new(x, y);
    rotated = true;
  }

  public override void Update(GameTime gameTime) {
    if (State == LockableDoorBlockState.Open) {
      currentFrame = sourceRects.Count - 1;
    }
  }

  public override void Draw(SpriteBatch spriteBatch) {
    if (!rotated) { Rotate(); }
    spriteBatch.Draw(
      SlattedDoorTexture,
      Position,
      sourceRects[currentFrame],
      Color.White,
      Rotation,
      Vector2.Zero,
      1.0f,
      SpriteEffects.None,
      0.0f
    );
  }

  public override void OnCollision(CollisionInfo info) {
    if (State == LockableDoorBlockState.Open && info.Collider is Player) {
      levelManager.SwitchLevel(PairedLevelName);
    }
  }

  public void ChangeState(LockableDoorBlockState state) {
    State = state;
  }
}
