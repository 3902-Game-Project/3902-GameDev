using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class OpenVaultDoorBlock : IBlock {
  private static Texture2D texture;
  private List<Rectangle> sourceRects;
  private int currentFrame;
  private double animationTimer;
  private double timePerFrame = 0.3;
  public float XPos { get; private set; }
  public float YPos { get; private set; }
  public string PairedLevelName { get; private set; }
  public BlockState State { get; set; }
  
  public Rectangle BoundingBox => new Rectangle((int)XPos, (int)YPos, (int)(sourceRects[0].Width * 2f), (int)(sourceRects[0].Height * 2f));

  public OpenVaultDoorBlock(Texture2D OpenVaultDoorTexture, Vector2 xyPos, string pairedLevelName) {
    texture = OpenVaultDoorTexture;
    currentFrame = 0;
    XPos = xyPos.X;
    YPos = xyPos.Y;
    PairedLevelName = pairedLevelName;
    State = BlockState.lit;
    sourceRects = new List<Rectangle> {
      new Rectangle(64, 192, 64, 64),
      new Rectangle(128, 192, 64, 64),
      new Rectangle(192, 192, 64, 64),
      new Rectangle(256, 192, 64, 64),
      new Rectangle(320, 192, 64, 64)
    };

    Vector2 dimensions = new Vector2(128, 128);

    Vector2 centerPosition = new Vector2(XPos + 64, YPos + 64);

    
  }

  public void Update(GameTime gameTime) {
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
    animationTimer += dt;
    if (animationTimer >= timePerFrame) { // && currentFrame < sourceRects.Count - 1
      currentFrame++;
      if (currentFrame == sourceRects.Count) { currentFrame = 0; }  // remove for final game
      animationTimer = 0;

    }
  }

  public void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, new Vector2(XPos, YPos), sourceRects[currentFrame],
                      Color.White, 0.0f, new Vector2(0, 0), 2.0f,
                      SpriteEffects.None, 0.0f);
  }
}
