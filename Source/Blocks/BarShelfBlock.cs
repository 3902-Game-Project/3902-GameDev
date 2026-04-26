using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class BarShelfBlock : ABaseBlock {
  private static Rectangle sourceRect = new(256, 256, 64, 64);
  private readonly Texture2D barShelfTexture;
  private float rotation = 0f;
  private bool rotated = false;

  public BarShelfBlock(Texture2D BarShelfTexture, Vector2 xyPos) : base(xyPos) {
    barShelfTexture = BarShelfTexture;

    Rotate();
  }

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(barShelfTexture, Position, sourceRect, Color.White, rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }

  public void Rotate() {
    float x = Position.X, y = Position.Y;
    if (Position.X >= 832) {
      rotation = MathHelper.ToRadians(90);
      x += 64;
      rotated = true;
    }

    Position = new(x, y);
  }
}
