using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class FirePitBlock : BaseBlock {
  private Texture2D texture;
  private Rectangle sourceRect;
  public BlockState State { get; set; }

  public FirePitBlock(Texture2D FirePitTexture, Vector2 xyPos) : base(xyPos) {
    texture = FirePitTexture;
    State = BlockState.extinguished;
    sourceRect = new Rectangle(320, 64, 64, 64);
  }

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
