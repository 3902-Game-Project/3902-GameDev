using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class RockHoleBlock : BaseBlock {
  private Texture2D texture;
  private Rectangle sourceRect;
  public string PairedLevelName { get; private set; }

  public RockHoleBlock(Texture2D RockHoleTexture, Vector2 xyPos, string pairedLevelName) : base(xyPos, 64f, 64f) {
    texture = RockHoleTexture;
    PairedLevelName = pairedLevelName;
    sourceRect = new Rectangle(384, 128, 64, 64);
  }

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
