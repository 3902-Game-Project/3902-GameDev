using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class LadderBlock(Texture2D LadderTexture, Vector2 xyPos) : ABaseBlock(xyPos) {
  private static Rectangle sourceRect = new(256, 0, 64, 64);

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(LadderTexture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
