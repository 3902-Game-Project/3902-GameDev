using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class WoodPlankBlock(Texture2D WoodPlankTexture, Vector2 xyPos) : ABaseBlock(xyPos) {
  private static Rectangle sourceRect = new(128, 128, 64, 64);

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(WoodPlankTexture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
