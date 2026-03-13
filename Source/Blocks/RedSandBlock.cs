using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class RedSandBlock : BaseBlock {
  private Texture2D texture;
  private Rectangle sourceRect;

  public RedSandBlock(Texture2D RedSandTexture, Vector2 xyPos) : base(xyPos) {
    texture = RedSandTexture;
    sourceRect = new Rectangle(64, 256, 64, 64);
  }

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
