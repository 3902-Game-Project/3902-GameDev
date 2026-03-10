using GameProject.Collisions;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class MudBlock : IBlock {
  private static Texture2D texture;
  private Rectangle sourceRect;
  public float XPos { get; private set; }
  public float YPos { get; private set; }
  
  public Rectangle BoundingBox => new Rectangle((int)XPos, (int)YPos, (int)(sourceRect.Width * 1f), (int)(sourceRect.Height * 1f));

  public MudBlock(Texture2D MudTexture, Vector2 xyPos) {
    texture = MudTexture;
    XPos = xyPos.X;
    YPos = xyPos.Y;
    sourceRect = new Rectangle(192, 0, 64, 64); // will be in xml (or something else) file later -Aaron

    Vector2 dimensions = new Vector2(64, 64);

    Vector2 centerPosition = new Vector2(XPos + 32, YPos + 32);

    
  }

  public void Update(GameTime gameTime) {
    // check for collision, player cannot cross
  }

  public void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, new Vector2(XPos, YPos), sourceRect,
                      Color.White, 0.0f, new Vector2(0, 0), 1.0f,
                      SpriteEffects.None, 0.0f);
  }
}
