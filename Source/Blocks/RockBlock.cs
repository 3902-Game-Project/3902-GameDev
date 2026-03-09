using System;
using System.Runtime.Intrinsics.X86;
using GameProject.Collisions;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class RockBlock : IBlock {
  private static Texture2D texture;
  private Rectangle sourceRect;
  private Vector2 centerPosition;
  public float XPos { get; private set; }
  public float YPos { get; private set; }
  public float Rotation { get; private set; }
  public ICollider Collider { get; private set; }
  public Rectangle BoundingBox => new Rectangle((int)XPos, (int)YPos, (int)(sourceRect.Width * 1f), (int)(sourceRect.Height * 1f));

  public RockBlock(Texture2D RockTexture, Vector2 xyPos) {
    texture = RockTexture;
    XPos = xyPos.X;
    YPos = xyPos.Y;
    sourceRect = new Rectangle(320, 0, 64, 64); // will be in xml (or something else) file later -Aaron
    //Rotate();
    Vector2 dimensions = new Vector2(64, 64);

    centerPosition = new Vector2(XPos + 32, YPos + 32);

    Collider = new BoxCollider(dimensions, centerPosition);
  }
  public void Rotate() {
    if (XPos > 0 && YPos > 0) {
      Rotation = (float)Math.PI;
    } else if (YPos > 0 && XPos == 0) {
      Rotation = 3f * (float)Math.PI / 2f;
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
