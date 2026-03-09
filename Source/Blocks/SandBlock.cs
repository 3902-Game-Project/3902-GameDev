using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class SandBlock : IBlock {
  private static Texture2D texture;
  private Rectangle sourceRect;
  public float XPos { get; private set; }
  public float YPos { get; private set; }
  public ICollider Collider { get; private set; }

  public Rectangle BoundingBox => new Rectangle((int)XPos, (int)YPos, (int)(sourceRect.Width * 1f), (int)(sourceRect.Height * 1f));

  public SandBlock(Texture2D sandTexture, Vector2 xyPos) {
    texture = sandTexture;
    XPos = xyPos.X;
    YPos = xyPos.Y;
    sourceRect = new Rectangle(0, 0, 64, 64); // will be in xml (or something else) file later -Aaron

    Vector2 dimensions = new Vector2(64, 64);

    Vector2 centerPosition = new Vector2(XPos + 32, YPos + 32);

    //Collider = new BoxCollider(dimensions, centerPosition); // no collision detection needed for the floor -Aaron
  }

  public void Update(GameTime gameTime) {
    // not needed
  }

  public void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, new Vector2(XPos, YPos), sourceRect,
                      Color.White, 0.0f, new Vector2(0, 0), 1.0f,
                      SpriteEffects.None, 0.0f);
  }
}
