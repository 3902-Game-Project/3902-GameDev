using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class LadderBlock : IBlock {
  private static Texture2D texture;
  private Rectangle sourceRect;
  public float XPos { get; private set; }
  public float YPos { get; private set; }
  public Rectangle BoundingBox => new Rectangle((int)XPos, (int)YPos, (int)(sourceRect.Width * 2f), (int)(sourceRect.Height * 2f));

  public LadderBlock(Texture2D LadderTexture, Vector2 xyPos) {
    texture = LadderTexture;
    XPos = xyPos.X;
    YPos = xyPos.Y;
    sourceRect = new Rectangle(256, 0, 63, 63); // will be in xml (or something else) file later -Aaron
  }

  public void Update(GameTime gameTime) {
    // check is player climbing
  }

  public void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, new Vector2(XPos, YPos), sourceRect,
                      Color.White, 0.0f, new Vector2(0, 0), 2.0f,
                      SpriteEffects.None, 0.0f);
  }
}
