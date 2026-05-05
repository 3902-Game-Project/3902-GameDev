using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class TableBlock(Texture2D TableTexture, Vector2 xyPos) : ABaseBlock(xyPos, height: 48.0f) {
  private static readonly Rectangle SOURCE_RECT = new(384, 208, 64, 48);

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(TableTexture, Position, SOURCE_RECT, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
