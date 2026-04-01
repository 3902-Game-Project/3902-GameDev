using System.Collections.Generic;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class FireBlock : BaseBlock {
  private Texture2D texture;
  private List<Rectangle> sourceRects;
  private int currentFrame;
  private double animationTimer;
  private double timePerFrame = 0.15;
  public BlockState State { get; set; }

  public FireBlock(Texture2D FireTexture, Vector2 xyPos) : base(xyPos) {
    texture = FireTexture;
    currentFrame = 0;
    State = BlockState.lit;
    sourceRects = new List<Rectangle> {
      new(384, 64, 64, 64),
      new(448, 64, 64, 64)
    };
  }

  public override void Update(GameTime gameTime) {
    float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
    animationTimer += dt;
    if (animationTimer >= timePerFrame) {
      currentFrame = 1 - currentFrame;
      animationTimer = 0;
    }
  }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRects[currentFrame], Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
