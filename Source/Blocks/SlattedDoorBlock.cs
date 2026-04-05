using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Interfaces;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class SlattedDoorBlock(Texture2D SlattedDoorTexture, Vector2 xyPos, BlockState state, string pairedLevelName, ILevelManager levelManager) : BaseBlock(xyPos) {
  private readonly int currentFrame = state switch {
    BlockState.open => 1,
    _ => 0,
  };
  private readonly List<Rectangle> sourceRects = [
      new(192, 128, 64, 64),
      new(320, 128, 64, 64)
    ];
  public float Rotation { get; private set; } = 0.0f;
  public string PairedLevelName { get; private set; } = pairedLevelName;
  public BlockState State { get; private set; } = state;

  public void Rotate() {
    if (Position.X == 0 && Position.Y == 0) {
      Rotation = MathHelper.ToRadians(270);
    } else if (Position.X > 0 && Position.Y > 0) {
      Rotation = MathHelper.ToRadians(90);
    } else if (Position.Y > 0 && Position.X == 0) {
      Rotation = MathHelper.ToRadians(180);
    }
  }

  public override void Update(GameTime gameTime) {
    // if player has key, change state to open
  }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(SlattedDoorTexture, Position, sourceRects[currentFrame], Color.White, Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }

  public override void OnCollision(CollisionInfo info) {
    if (State == BlockState.open && info.Collider is Player) {
      levelManager.SwitchLevel(PairedLevelName);
    }
  }
}
