using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class BarrelBlock : BaseBlock {
  private Texture2D texture;
  private Rectangle sourceRect;
  public BlockState State { get; set; }
  public BarrelBlock(Texture2D barrelTexture, Vector2 xyPos) : base(xyPos) {
    texture = barrelTexture;
    State = BlockState.solid;
    sourceRect = new Rectangle(64, 0, 64, 64);
  }

  public override void Update(GameTime gameTime) {
    // implement later
  }

  public override void Draw(SpriteBatch spriteBatch) {
    // We use the inherited 'Position' variable here to draw the sprite
    // exactly where the physical collision box is!
    spriteBatch.Draw(texture, Position, sourceRect,
                     Color.White, 0.0f, Vector2.Zero, 1.0f,
                     SpriteEffects.None, 0.0f);
  }

  public void ChangeState(BlockState state) {
    State = state;
  }
}
