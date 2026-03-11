using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class StatueBlock : BaseBlock {
  private Texture2D texture;
  private Rectangle sourceRect;

  public StatueBlock(Texture2D StatueTexture, Vector2 xyPos) : base(xyPos, 128f, 192f) {
    texture = StatueTexture;
    sourceRect = new Rectangle(0, 160, 64, 96);
  }

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
  }
}
