using GameProject.Collisions;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class OpenSlattedDoorBlock : IBlock {
  private static Texture2D texture;
  private Rectangle sourceRect;
  private Vector2 centerPosition;
  private Vector2 dimensions;
  public float XPos { get; private set; }
  public float YPos { get; private set; }
  public float Rotation { get; private set; }
  public string PairedLevelName { get; private set; }
  public ICollider Collider { get; private set; }
  public Rectangle BoundingBox => new Rectangle((int)XPos, (int)YPos, (int)(sourceRect.Width * 1f), (int)(sourceRect.Height * 1f));

  public OpenSlattedDoorBlock(Texture2D OpenSlattedDoorTexture, Vector2 xyPos, string pairedLevelName) {
    texture = OpenSlattedDoorTexture;
    XPos = xyPos.X;
    YPos = xyPos.Y;
    Rotation = 0.0f;
    PairedLevelName = pairedLevelName;
    sourceRect = new Rectangle(192, 128, 64, 64); // will be in xml (or something else) file later -Aaron
    //Rotate();
    dimensions = new Vector2(64, 64);

    centerPosition = new Vector2(XPos + 32, YPos + 32);

    //Collider = new BoxCollider(dimensions, centerPosition);
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
