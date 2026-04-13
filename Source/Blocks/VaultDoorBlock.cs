using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class VaultDoorBlock(Texture2D VaultDoorTexture, Vector2 xyPos, VaultDoorBlockState state, string pairedLevelName, ILevelManager levelManager) : BaseBlock(xyPos, 128f, 128f) {
  private static readonly List<Rectangle> sourceRects = [
    new(64, 128, 64, 64),
    new(64, 192, 64, 64),
    new(128, 192, 64, 64),
    new(192, 192, 64, 64),
    new(256, 192, 64, 64),
    new(320, 192, 64, 64)
  ];
  private int currentFrame = state switch {
    VaultDoorBlockState.Opening => 1,
    VaultDoorBlockState.Open => sourceRects.Count - 1,
    _ => 0,
  };
  private double animationTimer;
  private readonly double timePerFrame = 0.3;
  public string PairedLevelName { get; private set; } = pairedLevelName;
  public VaultDoorBlockState State { get; private set; } = state;

  public override void Update(GameTime gameTime) {
    if (State == VaultDoorBlockState.Opening) {
      float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
      animationTimer += dt;
      if (animationTimer >= timePerFrame) {
        currentFrame++;
        animationTimer = 0;
      }
      if (currentFrame >= sourceRects.Count) {
        State = VaultDoorBlockState.Open;
        currentFrame = sourceRects.Count - 1;
      }
    }
  }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(VaultDoorTexture, Position, sourceRects[currentFrame], Color.White, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
  }

  public override void OnCollision(CollisionInfo info) {
    if (State == VaultDoorBlockState.Open && info.Collider is Player) {
      levelManager.SwitchLevel(PairedLevelName);
    }
  }

  public void ChangeState(VaultDoorBlockState state) {
    State = state;
  }
}
