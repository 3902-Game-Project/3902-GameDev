using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class WoodCornerBlock(Texture2D WoodCornerTexture, Vector2 xyPos) : BaseBlock(xyPos) {
  private Rectangle sourceRect = new(128, 64, 64, 64);
  public float Rotation { get; private set; } = 0.0f;

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(WoodCornerTexture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
