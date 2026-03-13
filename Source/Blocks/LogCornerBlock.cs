using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class LogCornerBlock : BaseBlock {
  private Texture2D texture;
  private Rectangle sourceRect;

  public LogCornerBlock(Texture2D LogCornerTexture, Vector2 xyPos) : base(xyPos) {
    texture = LogCornerTexture;
    sourceRect = new Rectangle(192, 64, 64, 64);
  }

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
