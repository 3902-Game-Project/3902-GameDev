using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class CactusBlock : BaseBlock {
  private Texture2D texture;
  private Rectangle sourceRect;

  public CactusBlock(Texture2D CactusTexture, Vector2 xyPos) : base(xyPos, 128f, 128f) {
    texture = CactusTexture;
    sourceRect = new Rectangle(320, 256, 64, 64);
  }

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
  }
}
