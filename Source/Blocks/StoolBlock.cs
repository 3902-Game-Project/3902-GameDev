using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class StoolBlock(Texture2D StoolTexture, Vector2 xyPos) : ABaseBlock(xyPos, 21f, 36f) {
  private static readonly Rectangle sourceRect = new(470, 220, 21, 36);

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(StoolTexture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
