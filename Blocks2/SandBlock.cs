

using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks2;

public class SandBlock : IBlock{
  private static Texture2D texture;
  private Rectangle sourceRect;
  public int xPos { get; private set; }
  public int yPos { get; private set; }



  public SandBlock(Texture2D sandTexture, int x, int y) {
    texture = sandTexture;
    xPos = x;
    yPos = y;
    sourceRect = new Rectangle(0, 0, 63, 63); // will be in xml (or something else) file later -Aaron
  }
  public void Update(GameTime gameTime) {
    // not needed
  }

  public void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, new Vector2(xPos, yPos), sourceRect, 
                      Color.White, 0.0f, new Vector2(0, 0), 2.0f, 
                      SpriteEffects.None, 0.0f);
  }
}
