using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class TableBlock(Texture2D TableTexture, Vector2 xyPos) : ABaseBlock(xyPos, 64f, 48f) {
  private static Rectangle sourceRect = new(384, 208, 64, 48);
  

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(TableTexture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
