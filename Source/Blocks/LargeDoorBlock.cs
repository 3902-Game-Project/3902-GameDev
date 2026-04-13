using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class LargeDoorBlock(Texture2D LargeDoorTexture, Vector2 xyPos) : BaseBlock(xyPos, 64f, 128f) {
  private static Rectangle sourceRect = new(448, 320, 64, 128);

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(LargeDoorTexture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
