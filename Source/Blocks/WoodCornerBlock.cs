using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class WoodCornerBlock : BaseBlock {
  private Texture2D texture;
  private Rectangle sourceRect;
  public float Rotation { get; private set; }

  public WoodCornerBlock(Texture2D WoodCornerTexture, Vector2 xyPos) : base(xyPos) {
    texture = WoodCornerTexture;
    Rotation = 0.0f;
    sourceRect = new Rectangle(128, 64, 64, 64);
  }

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
