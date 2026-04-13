using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Interfaces;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class VaultDoorBlock : BaseBlock {
  private readonly Texture2D texture;
  private static readonly List<Rectangle> sourceRects = [
    new(64, 128, 64, 64),
    new(64, 192, 64, 64),
    new(128, 192, 64, 64),
    new(192, 192, 64, 64),
    new(256, 192, 64, 64),
    new(320, 192, 64, 64)
  ];
  private int currentFrame;
  private double animationTimer;
  private readonly double timePerFrame = 0.3;
  private readonly ILevelManager levelManager;
  public string PairedLevelName { get; private set; }
  public VaultDoorBlockState State { get; private set; }

  public VaultDoorBlock(Texture2D VaultDoorTexture, Vector2 xyPos, VaultDoorBlockState state, string pairedLevelName, ILevelManager levelManager) : base(xyPos, 128f, 128f) {
    texture = VaultDoorTexture;
    PairedLevelName = pairedLevelName;
    State = state;
    currentFrame = state switch {
      VaultDoorBlockState.Opening => 1,
      VaultDoorBlockState.Open => sourceRects.Count - 1,
      _ => 0,
    };
    this.levelManager = levelManager;
  }

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
    spriteBatch.Draw(texture, Position, sourceRects[currentFrame], Color.White, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
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
