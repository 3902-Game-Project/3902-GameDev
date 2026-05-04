using GameProject.Globals;
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
    if (x < Constants.BASE_BLOCK_WIDTH && y < Constants.BASE_BLOCK_HEIGHT) {
      Rotation = MathHelper.ToRadians(270);
      y += Constants.BASE_BLOCK_HEIGHT;
    } else if (x >= Constants.LEVEL_WIDTH - Constants.BASE_BLOCK_WIDTH && y >= Constants.BASE_BLOCK_HEIGHT) {
      Rotation = MathHelper.ToRadians(90);
      x += Constants.BASE_BLOCK_WIDTH;
    } else if (y >= Constants.LEVEL_HEIGHT - Constants.BASE_BLOCK_HEIGHT && x >= Constants.BASE_BLOCK_WIDTH) {
      Rotation = MathHelper.ToRadians(180);
      x += Constants.BASE_BLOCK_WIDTH;
      y += Constants.BASE_BLOCK_HEIGHT;
    }
    Position = new(x, y);
  }

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(rockCornerTexture, Position, sourceRect, Color.White, Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
