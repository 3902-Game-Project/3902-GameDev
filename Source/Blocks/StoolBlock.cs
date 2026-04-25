using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class StoolBlock(Texture2D StoolTexture, Vector2 xyPos) : ABaseBlock(xyPos) {
  private static Rectangle sourceRect = new(448, 192, 64, 64);

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(StoolTexture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
