

using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks2;

public class SandBlock : IBlock{
  private static Texture2D texture;
  private Rectangle sourceRect;

  public SandBlock() {

  }
  public void Update(GameTime gameTime) {
    // not needed
  }

  public void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, new Vector2(300, 200), sourceRect, Color.White, 0.0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0.0f);
  }
}
