using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks2;

public class SandBlock : IBlock{
  private static Texture2D texture;
  private Rectangle sourceRect;
  public float XPos { get; private set; }
  public float YPos { get; private set; }



  public SandBlock(Texture2D sandTexture, Vector2 xyPos) {
    texture = sandTexture;
    XPos = xyPos.X;
    YPos = xyPos.Y;
    sourceRect = new Rectangle(0, 0, 63, 63); // will be in xml (or something else) file later -Aaron
  }
  public void Update(GameTime gameTime) {
    // not needed
  }

  public void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, new Vector2(XPos, YPos), sourceRect, 
                      Color.White, 0.0f, new Vector2(0, 0), 2.0f, 
                      SpriteEffects.None, 0.0f);
  }
}
