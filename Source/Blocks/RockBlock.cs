using System;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class RockBlock : BaseBlock {
  private Texture2D texture;
  private Rectangle sourceRect;
  public float Rotation { get; private set; }

  public RockBlock(Texture2D RockTexture, Vector2 xyPos) : base(xyPos) {
    texture = RockTexture;
    sourceRect = new Rectangle(320, 0, 64, 64);
  }

  public void Rotate() {
    if (Position.X > 0 && Position.Y > 0) {
      Rotation = (float)Math.PI;
    } else if (Position.Y > 0 && Position.X == 0) {
      Rotation = 3f * (float)Math.PI / 2f;
    }
  }

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRect, Color.White, Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
