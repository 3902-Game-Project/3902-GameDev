using System.Collections.Generic;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class SlattedDoorBlock : BaseBlock {
  private Texture2D texture;
  private int currentFrame;
  private List<Rectangle> sourceRects;
  public float Rotation { get; private set; }
  public string PairedLevelName { get; private set; }
  public BlockState State { get; private set; }

  public SlattedDoorBlock(Texture2D SlattedDoorTexture, Vector2 xyPos, string pairedLevelName) : base(xyPos) {
    texture = SlattedDoorTexture;
    Rotation = 0.0f;
    PairedLevelName = pairedLevelName;
    currentFrame = 0;
    State = BlockState.locked;
    sourceRects = new List<Rectangle> {
      new Rectangle(192, 128, 64, 64),
      new Rectangle(320, 128, 64, 64)
    };
  }

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
    spriteBatch.Draw(texture, Position, sourceRects[currentFrame], Color.White, Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
