using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class CrateBlock : BaseBlock {
  private Texture2D texture;
  private Rectangle sourceRect;
  public BlockState State { get; set; }

  public CrateBlock(Texture2D CrateTexture, Vector2 xyPos) : base(xyPos) {
    texture = CrateTexture;
    State = BlockState.still;
    sourceRect = new Rectangle(128, 0, 64, 64);
  }

  public override void Update(GameTime gameTime) { 
    // update to handle collision
  }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }

  public void ChangeState(BlockState state) {
    State = state;
  }
}
