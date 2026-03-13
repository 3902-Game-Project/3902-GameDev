using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class TellersDeskBlock : BaseBlock {
  private Texture2D texture;
  private Rectangle sourceRect;

  public TellersDeskBlock(Texture2D TellersDeskTexture, Vector2 xyPos) : base(xyPos) {
    texture = TellersDeskTexture;
    sourceRect = new Rectangle(128, 320, 64, 64);
  }

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
