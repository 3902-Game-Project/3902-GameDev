using System.Collections.Generic;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class FireBlock(Texture2D FireTexture, Vector2 xyPos) : BaseBlock(xyPos) {
  private static readonly List<Rectangle> sourceRects = [
      new(384, 64, 64, 64),
      new(448, 64, 64, 64)
    ];
  private int currentFrame = 0;
  private double animationTimer;
  private readonly double timePerFrame = 0.15;
  public FirePitBlockState State { get; set; } = FirePitBlockState.Lit;

  public override void Update(GameTime gameTime) {
    float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
    animationTimer += dt;
    if (animationTimer >= timePerFrame) {
      currentFrame = 1 - currentFrame;
      animationTimer = 0;
    }
  }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(FireTexture, Position, sourceRects[currentFrame], Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
