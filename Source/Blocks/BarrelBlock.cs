using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public enum BarrelState { solid, breaking, broken }

public class BarrelBlock : IBlock {
  private static Texture2D texture;
  private Rectangle sourceRect;
  public float XPos { get; private set; }
  public float YPos { get; private set; }
  public BarrelState BarrelState { get; set; }
  public Rectangle BoundingBox => new Rectangle((int)XPos, (int)YPos, (int)(sourceRect.Width * 2f), (int)(sourceRect.Height * 2f));

  public BarrelBlock(Texture2D barrelTexture, Vector2 xyPos) {
    texture = barrelTexture;
    XPos = xyPos.X;
    YPos = xyPos.Y;
    BarrelState = BarrelState.solid;
    sourceRect = new Rectangle(64, 0, 63, 63); // will be in xml (or something else) file later -Aaron
  }

  public void Update(GameTime gameTime) {
    // implement later
  }

  public void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, new Vector2(XPos, YPos), sourceRect,
                      Color.White, 0.0f, new Vector2(0, 0), 2.0f,
                      SpriteEffects.None, 0.0f);
  }

  public void ChangeState(BarrelState state) {
    BarrelState = state;
    // implement rest later...
  }
}
