using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Source.Blocks;

public class RockCornerBlock : IBlock {
  private Texture2D texture;
  private Rectangle sourceRect;
  private Vector2 centerPosition;
  public float XPos { get; private set; }
  public float YPos { get; private set; }

  public float Rotation { get; private set; }

  public Rectangle BoundingBox => new Rectangle((int)XPos, (int)YPos, (int)(sourceRect.Width * 1f), (int)(sourceRect.Height * 1f));

  public RockCornerBlock(Texture2D RockCornerTexture, Vector2 xyPos) {
    texture = RockCornerTexture;
    XPos = xyPos.X;
    YPos = xyPos.Y;
    Rotation = 0.0f;
    sourceRect = new Rectangle(384, 0, 64, 64); // will be in xml (or something else) file later -Aaron
    //Rotate();
    Vector2 dimensions = new Vector2(64, 64); // changed from 2f scale - 128

    centerPosition = new Vector2(XPos + 32, YPos + 32); // changed from 2f scale - 64


  }

  public void Rotate() {
    if (XPos == 0 && YPos == 0) {
      Rotation = MathHelper.ToRadians(270);
    } else if (XPos > 0 && YPos > 0) {
      Rotation = MathHelper.ToRadians(90);
    } else if (YPos > 0 && XPos == 0) {
      Rotation = MathHelper.ToRadians(180);
    }
  }

  public void Update(GameTime gameTime) {
    // implement later
  }

  public void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, new Vector2(XPos, YPos), sourceRect,
                      Color.White, Rotation, new Vector2(0, 0), 1.0f,
                      SpriteEffects.None, 0.0f);
  }
}
