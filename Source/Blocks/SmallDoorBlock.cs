using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Managers;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class SmallDoorBlock : ABaseBlock {
  private static readonly List<Rectangle> sourceRects = [
    new(448, 256, 64, 64),
    new(448, 448, 64, 64)
  ];
  private readonly Texture2D smallDoorTexture;
  private readonly ILevelManager levelManager;
  private int currentFrame = 0;
  public float Rotation { get; private set; } = 0.0f;
  public string PairedLevelName { get; private set; }
  public LockableDoorBlockState State { get; private set; }

  public SmallDoorBlock(Texture2D smallDoorTexture, Vector2 xyPos, LockableDoorBlockState state, string pairedLevelName, ILevelManager levelManager) : base(xyPos) {
    this.smallDoorTexture = smallDoorTexture;
    this.levelManager = levelManager;

    PairedLevelName = pairedLevelName;
    State = state;

    if (State == LockableDoorBlockState.Open) {
      currentFrame = 1;
    }

    Rotate();
  }

  public void Rotate() {
    float x = Position.X, y = Position.Y;
    if (Position.X < Constants.BASE_BLOCK_WIDTH) {
      Rotation = MathHelper.ToRadians(90);
      x += Constants.BASE_BLOCK_WIDTH;
    } else if (Position.X >= 896 && Position.Y >= 64) {
      Rotation = MathHelper.ToRadians(90);
      x += Constants.BASE_BLOCK_WIDTH;
    }

    Position = new(x, y);
  }

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(smallDoorTexture, Position, sourceRects[currentFrame], Color.White, Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }

  public override void OnCollision(CollisionInfo info) {
    if (State == LockableDoorBlockState.Open && info.Collider is Player) {
      levelManager.SwitchLevel(PairedLevelName);
      SoundManager.Instance.Play(SoundID.Door);
    }
  }

  public void ChangeState(LockableDoorBlockState state) {
    State = state;

    if (State == LockableDoorBlockState.Open) {
      currentFrame = 1;
    }
  }
}
