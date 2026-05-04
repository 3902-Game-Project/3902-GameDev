using GameProject.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class StatueBlock(Texture2D StatueTexture, Vector2 xyPos) :
  ABaseBlock(xyPos, Constants.BASE_BLOCK_WIDTH * 2.0f, Constants.BASE_BLOCK_HEIGHT * 3.0f) {
  private static Rectangle sourceRect = new(0, 160, 64, 96);

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(StatueTexture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
  }
}
