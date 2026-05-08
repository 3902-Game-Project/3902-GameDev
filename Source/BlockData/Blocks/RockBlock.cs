using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class RockBlock(Texture2D RockTexture, Vector2 xyPos) : ABaseBlock(xyPos) {
  private static readonly Rectangle SOURCE_RECT = new(320, 0, 64, 64);

  internal float Rotation { get; private set; }

  internal void Rotate() {
    if (Position.X > 0 && Position.Y > 0) {
      Rotation = (float) Math.PI;
    } else if (Position.Y > 0 && Position.X == 0) {
      Rotation = 3f * (float) Math.PI / 2f;
    }
  }

  internal override void Update(double deltaTime) { }

  internal override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(RockTexture, Position, SOURCE_RECT, Color.White, Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
