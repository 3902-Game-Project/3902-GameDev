using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class ShelfBlock(Texture2D ShelfTexture, Vector2 xyPos) : ABaseBlock(xyPos, 64f, 64f) {
  private static Rectangle sourceRect = new(128, 256, 64, 64);

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(ShelfTexture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
