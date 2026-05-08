using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class RockBlock(Texture2D RockTexture, Vector2 xyPos) : ABaseBlock(xyPos) {
  private static readonly Rectangle SOURCE_RECT = new(320, 0, 64, 64);

  public float Rotation { get; private set; }

  public void Rotate() {
    if (Position.X > 0.0f && Position.Y > 0.0f) {
      Rotation = MathF.PI;
    } else if (Position.Y > 0.0f && Position.X == 0.0f) {
      Rotation = 3.0f * MathF.PI * 0.5f;
    }
  }

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(RockTexture, Position, SOURCE_RECT, Color.White, Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
