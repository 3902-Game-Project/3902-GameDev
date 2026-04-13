using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class TreasureBlock(Texture2D TreasureTexture, Vector2 xyPos) : BaseBlock(xyPos, 128f, 128f) {
  private Rectangle sourceRect = new(256, 448, 64, 64);

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(TreasureTexture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
  }
}
