using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class LogCornerBlock(Texture2D LogCornerTexture, Vector2 xyPos) : BaseBlock(xyPos) {
  private static Rectangle sourceRect = new(192, 64, 64, 64);
  private bool rotated = false;
  public float Rotation { get; private set; } = 0.0f;

  public void Rotate() {
    float x = Position.X, y = Position.Y;
    if (x < 64 && y >= 512) {
      Rotation = MathHelper.ToRadians(270);
      y += 64;
    } else if (x >= 896 && y >= 512) {
      Rotation = MathHelper.ToRadians(180);
      x += 64;
      y += 64;
    } else if (y < 64 && x >= 896) {
      Rotation = MathHelper.ToRadians(90);
      x += 64;
    }
    Position = new(x, y);
    rotated = true;
  }

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    if (!rotated) { Rotate(); }
    spriteBatch.Draw(LogCornerTexture, Position, sourceRect, Color.White, Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
