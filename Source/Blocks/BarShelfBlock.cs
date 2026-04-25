using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class BarShelfBlock(Texture2D BarShelfTexture, Vector2 xyPos) : ABaseBlock(xyPos) {
  private static Rectangle sourceRect = new(256, 256, 64, 64);

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(BarShelfTexture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
