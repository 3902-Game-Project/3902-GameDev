using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class CrateBlock(Texture2D CrateTexture, Vector2 xyPos) : BaseBlock(xyPos) {
  private Rectangle sourceRect = new(128, 0, 64, 64);
  public BlockState State { get; set; } = BlockState.still;

  public override void Update(GameTime gameTime) {
    // update to handle collision
  }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(CrateTexture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }

  public void ChangeState(BlockState state) {
    State = state;
  }
}
