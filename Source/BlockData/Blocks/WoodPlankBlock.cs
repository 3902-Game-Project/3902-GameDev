using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class WoodPlankBlock(Texture2D WoodPlankTexture, Vector2 xyPos) : ABaseBlock(xyPos) {
  private static readonly Rectangle SOURCE_RECT = new(128, 128, 64, 64);

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(WoodPlankTexture, Position, SOURCE_RECT, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
