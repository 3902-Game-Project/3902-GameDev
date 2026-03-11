using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class LockedVaultBlock : BaseBlock {
  private Texture2D texture;
  private Rectangle sourceRect;
  public string PairedLevelName { get; private set; }

  public LockedVaultBlock(Texture2D LockedVaultTexture, Vector2 xyPos, string pairedLevelName) : base(xyPos, 128f, 128f) {
    texture = LockedVaultTexture;
    PairedLevelName = pairedLevelName;
    sourceRect = new Rectangle(64, 128, 64, 64);
  }

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRect, Color.White, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);
  }
}
