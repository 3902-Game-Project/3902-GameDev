using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class ShelfBlock : BaseBlock {
  private Texture2D texture;
  private Rectangle sourceRect;

  public ShelfBlock(Texture2D ShelfTexture, Vector2 xyPos) : base(xyPos, 64f, 64f) {
    texture = ShelfTexture;
    sourceRect = new Rectangle(128, 256, 64, 64);
  }

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
