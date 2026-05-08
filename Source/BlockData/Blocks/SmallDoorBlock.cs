using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Commands;
using GameProject.Globals;
using GameProject.Misc;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class SmallDoorBlock : ABaseBlock {
  private static readonly List<Rectangle> SOURCE_RECTS = [
    new(448, 256, 64, 64),
    new(448, 448, 64, 64),
  ];

  private readonly Texture2D smallDoorTexture;
  private readonly IGPCommand changeLevelCommand;
  private int currentFrame = 0;

  internal float Rotation { get; private set; } = 0.0f;
  internal string PairedLevelName { get; private set; }
  internal LockableDoorBlockState State { get; private set; }

  internal SmallDoorBlock(Texture2D smallDoorTexture, Vector2 xyPos, LockableDoorBlockState state, IGPCommand changeLevelCommand) : base(xyPos) {
    this.smallDoorTexture = smallDoorTexture;
    this.changeLevelCommand = changeLevelCommand;

    State = state;

    if (State == LockableDoorBlockState.Open) {
      currentFrame = 1;
    }

    Rotate();
  }

  internal void Rotate() {
    float x = Position.X, y = Position.Y;
    if (Position.X < Constants.BASE_BLOCK_WIDTH) {
      Rotation = MathHelper.ToRadians(90);
      x += Constants.BASE_BLOCK_WIDTH;
    } else if (Position.X >= Constants.LEVEL_WIDTH - Constants.BASE_BLOCK_WIDTH && Position.Y >= Constants.BASE_BLOCK_HEIGHT) {
      Rotation = MathHelper.ToRadians(90);
      x += Constants.BASE_BLOCK_WIDTH;
    }

    Position = new(x, y);
  }

  internal override void Update(double deltaTime) { }

  internal override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(smallDoorTexture, Position, SOURCE_RECTS[currentFrame], Color.White, Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }

  internal override void OnCollision(CollisionInfo info) {
    if (State == LockableDoorBlockState.Open && info.Collider is Player) {
      changeLevelCommand.Execute();
      SoundManager.Instance.Play(SoundID.Door);
    }
  }

  internal void ChangeState(LockableDoorBlockState state) {
    State = state;

    if (State == LockableDoorBlockState.Open) {
      currentFrame = 1;
    }
  }
}
