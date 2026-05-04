using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class ShelfBlock(Texture2D ShelfTexture, Vector2 xyPos) : ABaseBlock(xyPos, height: 37.0f) {
  private static Rectangle sourceRect = new(128, 283, 64, 37);

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(ShelfTexture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
