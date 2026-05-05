using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class BankShelfBlock(Texture2D BankShelfTexture, Vector2 xyPos) : ABaseBlock(xyPos) {
  private static readonly Rectangle SOURCE_RECT = new(64, 320, 64, 64);

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(BankShelfTexture, Position, SOURCE_RECT, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
