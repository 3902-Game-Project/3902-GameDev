using GameProject.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class TreasureBlock(Texture2D TreasureTexture, Vector2 xyPos) :
  ABaseBlock(xyPos, Constants.BASE_BLOCK_WIDTH * 2.0f, Constants.BASE_BLOCK_HEIGHT * 2.0f) {
  private static readonly Rectangle sourceRect = new(256, 448, 64, 64);

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(TreasureTexture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
  }
}
