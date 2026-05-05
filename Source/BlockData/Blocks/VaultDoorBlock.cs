using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Globals;
using GameProject.Level;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class VaultDoorBlock(Texture2D VaultDoorTexture, Vector2 xyPos, VaultDoorBlockState state, string pairedLevelName, ILevelManager levelManager) :
  ABaseBlock(xyPos, Constants.BASE_BLOCK_WIDTH * 2.0f, Constants.BASE_BLOCK_HEIGHT * 2.0f) {
  private static readonly List<Rectangle> SOURCE_RECTS = [
    new(64, 128, 64, 64),
    new(64, 192, 64, 64),
    new(128, 192, 64, 64),
    new(192, 192, 64, 64),
    new(256, 192, 64, 64),
    new(320, 192, 64, 64),
  ];
  private static readonly double TIME_PER_FRAME = 0.1;

  private int currentFrame = state switch {
    VaultDoorBlockState.Opening => 1,
    VaultDoorBlockState.Open => SOURCE_RECTS.Count - 1,
    _ => 0,
  };

  private double animationTimer = 0.0f;

  public string PairedLevelName { get; private set; } = pairedLevelName;
  public VaultDoorBlockState State { get; private set; } = state;

  public override void Update(double deltaTime) {
    if (State == VaultDoorBlockState.Opening) {
      animationTimer += deltaTime;
      if (animationTimer >= TIME_PER_FRAME) {
        currentFrame++;
        animationTimer -= TIME_PER_FRAME;
      }
      if (currentFrame >= SOURCE_RECTS.Count) {
        State = VaultDoorBlockState.Open;
        currentFrame = SOURCE_RECTS.Count - 1;
      }
    }
  }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(VaultDoorTexture, Position, SOURCE_RECTS[currentFrame], Color.White, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
  }

  public override void OnCollision(CollisionInfo info) {
    if (State == VaultDoorBlockState.Open && info.Collider is Player) {
      levelManager.SwitchLevel(PairedLevelName);
    }
  }

  public void ChangeState(VaultDoorBlockState state) {
    State = state;
  }

  public void Unlock() {
    ChangeState(VaultDoorBlockState.Opening);
  }
}
