using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class ShelfBlock(Texture2D ShelfTexture, Vector2 xyPos) : ABaseBlock(xyPos, height: 37.0f) {
  private static readonly Rectangle SOURCE_RECT = new(128, 283, 64, 37);

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(ShelfTexture, Position, SOURCE_RECT, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
