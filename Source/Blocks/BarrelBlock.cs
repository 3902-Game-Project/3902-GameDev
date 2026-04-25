using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

internal class BarrelBlock(Texture2D barrelTexture, Vector2 xyPos) : ABaseBlock(xyPos) {
  private static Rectangle sourceRect = new(64, 0, 64, 64);
  public BarrelBlockState State { get; set; } = BarrelBlockState.Solid;

  public override void Update(double deltaTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    // We use the inherited 'Position' variable here to draw the sprite
    // exactly where the physical collision box is!
    spriteBatch.Draw(
      texture: barrelTexture,
      position: Position,
      sourceRectangle: sourceRect,
      color: Color.White,
      rotation: 0.0f,
      origin: Vector2.Zero,
      scale: 1.0f,
      effects: SpriteEffects.None,
      layerDepth: 0.0f
    );
  }

  public void ChangeState(BarrelBlockState state) {
    State = state;
  }
}
