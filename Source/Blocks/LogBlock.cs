using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class LogBlock : BaseBlock {
  private static Rectangle sourceRect = new(256, 64, 64, 64);
  private readonly Texture2D logTexture;

  public float Rotation { get; private set; } = 0.0f;

  public LogBlock(Texture2D LogTexture, Vector2 xyPos) : base(xyPos) {
    logTexture = LogTexture;

    Rotate();
  }

  public void Rotate() {
    float x = Position.X, y = Position.Y;
    if (y < 64) {
      Rotation = MathHelper.ToRadians(90);
      x += 64;
    } else if (y >= 512) {
      Rotation = MathHelper.ToRadians(90);
      x += 64;
    } else if (y < 128 && x >= 64 && x < 896) {
      Rotation = MathHelper.ToRadians(90);
      x += 64;
    }
    Position = new(x, y);
  }

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(logTexture, Position, sourceRect, Color.White, Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
