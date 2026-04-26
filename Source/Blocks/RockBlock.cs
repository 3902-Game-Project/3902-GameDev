using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class RockBlock(Texture2D RockTexture, Vector2 xyPos) : ABaseBlock(xyPos) {
  private static Rectangle sourceRect = new(320, 0, 64, 64);
  public float Rotation { get; private set; }

  public void Rotate() {
    if (Position.X > 0 && Position.Y > 0) {
      Rotation = (float) Math.PI;
    } else if (Position.Y > 0 && Position.X == 0) {
      Rotation = 3f * (float) Math.PI / 2f;
    }
  }

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(RockTexture, Position, sourceRect, Color.White, Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
