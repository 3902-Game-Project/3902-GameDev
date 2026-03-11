using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class LargeDoorBlock : BaseBlock {
  private Texture2D texture;
  private Rectangle sourceRect;

  public LargeDoorBlock(Texture2D LargeDoorTexture, Vector2 xyPos) : base(xyPos, 64f, 128f) {
    texture = LargeDoorTexture;
    sourceRect = new Rectangle(448, 320, 64, 128);
  }

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
