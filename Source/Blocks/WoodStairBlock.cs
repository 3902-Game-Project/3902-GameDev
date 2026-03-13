using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class WoodStairBlock : BaseBlock {
  private Texture2D texture;
  private Rectangle sourceRect;

  public WoodStairBlock(Texture2D WoodStairTexture, Vector2 xyPos) : base(xyPos) {
    texture = WoodStairTexture;
    sourceRect = new Rectangle(64, 64, 64, 64);
  }

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
