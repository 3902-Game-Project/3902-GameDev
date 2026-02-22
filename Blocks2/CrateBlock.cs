using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks2;

public enum CrateState { solid, breaking, broken }

public class CrateBlock : IBlock {
  private static Texture2D texture;
  private Rectangle sourceRect;
  public float XPos { get; private set; }
  public float YPos { get; private set; }
  public CrateState CrateState { get; set; }

  public CrateBlock(Texture2D CrateTexture, Vector2 xyPos) {
    texture = CrateTexture;
    XPos = xyPos.X;
    YPos = xyPos.Y;
    CrateState = CrateState.solid;
    sourceRect = new Rectangle(128, 0, 63, 63); // will be in xml (or something else) file later -Aaron
  }

  public void Update(GameTime gameTime) {
    // implement later
  }

  public void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, new Vector2(XPos, YPos), sourceRect,
                      Color.White, 0.0f, new Vector2(0, 0), 2.0f,
                      SpriteEffects.None, 0.0f);
  }

  public void ChangeState(CrateState state) {
    CrateState = state;
    // implement rest later...
  }
}
