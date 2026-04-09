using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks; // Note: Kept your specific namespace here!

public class RockCornerBlock(Texture2D RockCornerTexture, Vector2 xyPos) : BaseBlock(xyPos) {
  private Rectangle sourceRect = new(384, 0, 64, 64);
  private Boolean rotated = false;
  public float Rotation { get; private set; } = 0.0f;

  public void Rotate() {
    float x = Position.X, y = Position.Y;
    if (x < 64 && y < 64) {
      Rotation = MathHelper.ToRadians(270);
      y += 64;
    } else if (x >= 896 && y >= 64) {
      Rotation = MathHelper.ToRadians(90);
      x += 64;
    } else if (y >= 512 && x >= 64) {
      Rotation = MathHelper.ToRadians(180);
      x += 64;
      y += 64;
    }
    Position = new(x, y);
    rotated = true;
  }

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    if (!rotated) { Rotate(); }
    spriteBatch.Draw(RockCornerTexture, Position, sourceRect, Color.White, Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
