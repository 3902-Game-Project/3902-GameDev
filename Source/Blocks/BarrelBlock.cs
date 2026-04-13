using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class BarrelBlock(Texture2D barrelTexture, Vector2 xyPos) : BaseBlock(xyPos) {
  private static Rectangle sourceRect = new(64, 0, 64, 64);
  public BarrelBlockState State { get; set; } = BarrelBlockState.Solid;

  public override void Update(GameTime gameTime) {
    // implement later
  }

  public override void Draw(SpriteBatch spriteBatch) {
    // We use the inherited 'Position' variable here to draw the sprite
    // exactly where the physical collision box is!
    spriteBatch.Draw(barrelTexture, Position, sourceRect,
                     Color.White, 0.0f, Vector2.Zero, 1.0f,
                     SpriteEffects.None, 0.0f);
  }

  public void ChangeState(BarrelBlockState state) {
    State = state;
  }
}
