using GameProject.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class LogCornerBlock : ABaseBlock {
  private static readonly Rectangle SOURCE_RECT = new(192, 64, 64, 64);
  
  private readonly Texture2D logCornerTexture;

  public float Rotation { get; private set; } = 0.0f;

  public LogCornerBlock(Texture2D LogCornerTexture, Vector2 xyPos) : base(xyPos) {
    logCornerTexture = LogCornerTexture;

    Rotate();
  }

  public void Rotate() {
    float x = Position.X, y = Position.Y;
    if (x < Constants.BASE_BLOCK_WIDTH && y >= Constants.LEVEL_HEIGHT - Constants.BASE_BLOCK_HEIGHT) {
      Rotation = MathHelper.ToRadians(270);
      y += Constants.BASE_BLOCK_HEIGHT;
    } else if (x >= Constants.LEVEL_WIDTH - Constants.BASE_BLOCK_WIDTH && y >= Constants.LEVEL_HEIGHT - Constants.BASE_BLOCK_HEIGHT) {
      Rotation = MathHelper.ToRadians(180);
      x += Constants.BASE_BLOCK_WIDTH;
      y += Constants.BASE_BLOCK_HEIGHT;
    } else if (y < Constants.BASE_BLOCK_HEIGHT && x >= Constants.LEVEL_WIDTH - Constants.BASE_BLOCK_WIDTH) {
      Rotation = MathHelper.ToRadians(90);
      x += Constants.BASE_BLOCK_WIDTH;
    }
    Position = new(x, y);
  }

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(logCornerTexture, Position, SOURCE_RECT, Color.White, Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
