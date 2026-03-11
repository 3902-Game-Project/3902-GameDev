using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class StoolBlock : BaseBlock {
  private Texture2D texture;
  private Rectangle sourceRect;

  public StoolBlock(Texture2D StoolTexture, Vector2 xyPos) : base(xyPos) {
    texture = StoolTexture;
    sourceRect = new Rectangle(448, 192, 64, 64);
  }

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
