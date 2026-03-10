using GameProject.Collisions;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class RedXRockBlock : IBlock {
  private static Texture2D texture;
  private Rectangle sourceRect;
  public float XPos { get; private set; }
  public float YPos { get; private set; }
  
  public Rectangle BoundingBox => new Rectangle((int)XPos, (int)YPos, (int)(sourceRect.Width * 1f), (int)(sourceRect.Height * 1f));

  public RedXRockBlock(Texture2D RedXRockTexture, Vector2 xyPos) {
    texture = RedXRockTexture;
    XPos = xyPos.X;
    YPos = xyPos.Y;
    sourceRect = new Rectangle(448, 128, 64, 64); // will be in xml (or something else) file later -Aaron

    Vector2 dimensions = new Vector2(64, 64);

    Vector2 centerPosition = new Vector2(XPos + 32, YPos + 32);

    
  }

  public void Update(GameTime gameTime) {
    // implement later
  }

  public void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, new Vector2(XPos, YPos), sourceRect,
                      Color.White, 0.0f, new Vector2(0, 0), 1.0f,
                      SpriteEffects.None, 0.0f);
  }
}
