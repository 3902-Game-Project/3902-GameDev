using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class FireBlock : IBlock {
  private static Texture2D texture;
  private List<Rectangle> sourceRects;
  private int currentFrame;
  private double animationTimer;
  private double timePerFrame = 0.15;
  public float XPos { get; private set; }
  public float YPos { get; private set; }
  public BlockState State { get; set; }
  public ICollider Collider { get; private set; }
  public Rectangle BoundingBox => new Rectangle((int)XPos, (int)YPos, (int)(sourceRects[0].Width * 1f), (int)(sourceRects[0].Height * 1f));

  public FireBlock(Texture2D FireTexture, Vector2 xyPos) {
    texture = FireTexture;
    currentFrame = 0;
    XPos = xyPos.X;
    YPos = xyPos.Y;
    State = BlockState.lit;
    sourceRects = new List<Rectangle> { 
      new Rectangle(384, 64, 64, 64),
      new Rectangle(448, 64, 64, 64)
    };

    Vector2 dimensions = new Vector2(64, 64);

    Vector2 centerPosition = new Vector2(XPos + 32, YPos + 32);

    Collider = new BoxCollider(dimensions, centerPosition);
  }

  public void Update(GameTime gameTime) {
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
    animationTimer += dt;
    if (animationTimer >= timePerFrame) {
        currentFrame = 1 - currentFrame;
        animationTimer = 0;
    }
  }

  public void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, new Vector2(XPos, YPos), sourceRects[currentFrame],
                      Color.White, 0.0f, new Vector2(0, 0), 1.0f,
                      SpriteEffects.None, 0.0f);
  }
}
