using GameProject.Blocks;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Source.Blocks; // Note: Kept your specific namespace here!

public class RockCornerBlock : BaseBlock {
  private Texture2D texture;
  private Rectangle sourceRect;
  public float Rotation { get; private set; }

  public RockCornerBlock(Texture2D RockCornerTexture, Vector2 xyPos) : base(xyPos) {
    texture = RockCornerTexture;
    Rotation = 0.0f;
    sourceRect = new Rectangle(384, 0, 64, 64);
  }

  public void Rotate() {
    if (Position.X == 0 && Position.Y == 0) {
      Rotation = MathHelper.ToRadians(270);
    } else if (Position.X > 0 && Position.Y > 0) {
      Rotation = MathHelper.ToRadians(90);
    } else if (Position.Y > 0 && Position.X == 0) {
      Rotation = MathHelper.ToRadians(180);
    }
  }

  public override void Update(GameTime gameTime) { }

  public override void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRect, Color.White, Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
  }
}
