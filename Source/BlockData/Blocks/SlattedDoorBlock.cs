using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Commands;
using GameProject.Globals;
using GameProject.Level;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class SlattedDoorBlock : ABaseBlock {
  private static readonly List<Rectangle> SOURCE_RECTS = [
    new(192, 128, 64, 64),
    new(320, 128, 64, 64),
  ];

  private readonly Texture2D slattedDoorTexture;
  private readonly IGPCommand changeLevelCommand;
  private int currentFrame = 0;

  public float Rotation { get; private set; } = 0.0f;
  public LockableDoorBlockState State { get; private set; }

  public SlattedDoorBlock(Texture2D SlattedDoorTexture, Vector2 xyPos, LockableDoorBlockState state, IGPCommand changeLevelCommand) : base(xyPos) {
    slattedDoorTexture = SlattedDoorTexture;
    this.changeLevelCommand = changeLevelCommand;

    State = state;

    if (State == LockableDoorBlockState.Open) {
      currentFrame = SOURCE_RECTS.Count - 1;
    }

    Rotate();
  }

  public void Rotate() {
    float x = Position.X, y = Position.Y;
    if (x < Constants.BASE_BLOCK_WIDTH) {
      Rotation = MathHelper.ToRadians(270);
      y += Constants.BASE_BLOCK_HEIGHT;
    } else if (x >= Constants.LEVEL_WIDTH - Constants.BASE_BLOCK_WIDTH && y >= Constants.BASE_BLOCK_HEIGHT) {
      Rotation = MathHelper.ToRadians(90);
      x += Constants.BASE_BLOCK_WIDTH;
    } else if (y >= Constants.LEVEL_HEIGHT - Constants.BASE_BLOCK_HEIGHT && x >= Constants.BASE_BLOCK_WIDTH) {
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
      SOURCE_RECTS[currentFrame],
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
      changeLevelCommand.Execute();
    }
  }

  public void ChangeState(LockableDoorBlockState state) {
    State = state;

    if (State == LockableDoorBlockState.Open) {
      currentFrame = SOURCE_RECTS.Count - 1;
    }
  }

  public void Unlock() {
    ChangeState(LockableDoorBlockState.Open);
  }
}
