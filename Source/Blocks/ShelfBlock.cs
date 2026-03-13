using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class ShelfBlock : BaseBlock {
  private Texture2D texture;
  private Rectangle sourceRect;

  public ShelfBlock(Texture2D ShelfTexture, Vector2 xyPos) : base(xyPos, 126f, 126f) {
    texture = ShelfTexture;
    sourceRect = new Rectangle(128, 256, 63, 63);
  }

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
  }
}
