using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class LogCornerBlock : BaseBlock {
  private static Rectangle sourceRect = new(192, 64, 64, 64);
  private readonly Texture2D logCornerTexture;

  public float Rotation { get; private set; } = 0.0f;

  public LogCornerBlock(Texture2D LogCornerTexture, Vector2 xyPos) : base(xyPos) {
    logCornerTexture = LogCornerTexture;

    Rotate();
  }

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
  }

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(logCornerTexture, Position, sourceRect, Color.White, Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
