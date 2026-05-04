using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Managers;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class SlattedDoorBlock : ABaseBlock {
  private int currentFrame = 0;
  private static readonly List<Rectangle> sourceRects = [
      new(192, 128, 64, 64),
      new(320, 128, 64, 64)
    ];
  private readonly Texture2D slattedDoorTexture;
  private readonly ILevelManager levelManager;

  public float Rotation { get; private set; } = 0.0f;
  public string PairedLevelName { get; private set; }
  public LockableDoorBlockState State { get; private set; }

  public SlattedDoorBlock(Texture2D SlattedDoorTexture, Vector2 xyPos, LockableDoorBlockState state, string pairedLevelName, ILevelManager levelManager) : base(xyPos) {
    slattedDoorTexture = SlattedDoorTexture;
    this.levelManager = levelManager;
    PairedLevelName = pairedLevelName;
    State = state;

    Rotate();

    if (State == LockableDoorBlockState.Open) {
      currentFrame = sourceRects.Count - 1;
    }
  }

  public void Rotate() {
    float x = Position.X, y = Position.Y;
    if (x < Constants.BASE_BLOCK_WIDTH) {
      Rotation = MathHelper.ToRadians(270);
      y += Constants.BASE_BLOCK_HEIGHT;
    } else if (x >= 896 && y >= 64) {
      Rotation = MathHelper.ToRadians(90);
      x += Constants.BASE_BLOCK_WIDTH;
    } else if (y >= 512 && x >= 64) {
      Rotation = MathHelper.ToRadians(180);
      x += Constants.BASE_BLOCK_WIDTH;
      y += Constants.BASE_BLOCK_HEIGHT;
    }
    Position = new(x, y);
  }

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(
      slattedDoorTexture,
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

    if (State == LockableDoorBlockState.Open) {
      currentFrame = sourceRects.Count - 1;
    }
  }

  public void Unlock() {
    ChangeState(LockableDoorBlockState.Open);
  }
}
