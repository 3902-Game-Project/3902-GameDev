using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class SmallDoorBlock(Texture2D SmallDoorTexture, Vector2 xyPos, BlockState state, string pairedLevelName, ILevelManager levelManager) : BaseBlock(xyPos) {
  private readonly int currentFrame = state switch {
    BlockState.open => 1,
    _ => 0,
  };
  private readonly List<Rectangle> sourceRects = [
      new(448, 256, 64, 64),
      new(448, 448, 64, 64)
    ];
  public float Rotation { get; private set; } = 0.0f;
  private bool rotated = false;
  public string PairedLevelName { get; private set; } = pairedLevelName;
  public BlockState State { get; private set; } = state;

  public void Rotate() {
    float x = Position.X, y = Position.Y;
    if (Position.X == 0 ) {
      Rotation = MathHelper.ToRadians(90);
      x += 64;
      //y = Position.Y + 64;
    } else if (Position.X > 0 && Position.Y > 0) {
      Rotation = MathHelper.ToRadians(90);
      x += 64;
      y += 64;
    } else if (Position.Y > 0 && Position.X == 0) {
      Rotation = MathHelper.ToRadians(90);
      x -= 64;
      y += 64;
    }
    Position = new(x, y);
    rotated = true;
  }
  
  public override void Update(GameTime gameTime) {
    // if all enemies defeated, change state to open
  }

  public override void Draw(SpriteBatch spriteBatch) {
    //if (!rotated) { this.Rotate(); }
    spriteBatch.Draw(SmallDoorTexture, Position, sourceRects[currentFrame], Color.White, Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }

  public override void OnCollision(CollisionInfo info) {
    if (State == BlockState.open && info.Collider is Player) {
      levelManager.SwitchLevel(PairedLevelName);
    }
  }
}
