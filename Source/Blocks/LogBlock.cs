using GameProject.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class LogBlock : ABaseBlock {
  private static Rectangle sourceRect = new(256, 64, 64, 64);
  private readonly Texture2D logTexture;

  public float Rotation { get; private set; } = 0.0f;

  public LogBlock(Texture2D LogTexture, Vector2 xyPos) : base(xyPos) {
    logTexture = LogTexture;

    Rotate();
  }

  public void Rotate() {
    float x = Position.X, y = Position.Y;
    // Someone else did these offsets but are they correct? they seem awfully arbitrary - Santosh
    if (y < Constants.BASE_BLOCK_HEIGHT) {
      Rotation = MathHelper.ToRadians(90);
      x += Constants.BASE_BLOCK_WIDTH;
    } else if (y >= Constants.LEVEL_HEIGHT - Constants.BASE_BLOCK_HEIGHT) {
      Rotation = MathHelper.ToRadians(90);
      x += Constants.BASE_BLOCK_WIDTH;
    } else if (y < Constants.BASE_BLOCK_HEIGHT * 2.0f && x >= Constants.BASE_BLOCK_WIDTH && x < Constants.LEVEL_WIDTH - Constants.BASE_BLOCK_WIDTH) {
      Rotation = MathHelper.ToRadians(90);
      x += Constants.BASE_BLOCK_WIDTH;
    }
    Position = new(x, y);
  }

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(logTexture, Position, sourceRect, Color.White, Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
