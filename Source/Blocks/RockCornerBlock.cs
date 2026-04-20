using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks; // Note: Kept your specific namespace here!

internal class RockCornerBlock : ABaseBlock {
  private static Rectangle sourceRect = new(384, 0, 64, 64);
  private readonly Texture2D rockCornerTexture;

  public float Rotation { get; private set; } = 0.0f;

  public RockCornerBlock(Texture2D RockCornerTexture, Vector2 xyPos) : base(xyPos) {
    rockCornerTexture = RockCornerTexture;

    Rotate();
  }

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
  }

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(rockCornerTexture, Position, sourceRect, Color.White, Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
