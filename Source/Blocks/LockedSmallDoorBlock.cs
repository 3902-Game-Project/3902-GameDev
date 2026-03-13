using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class LockedSmallDoorBlock : BaseBlock {
  private Texture2D texture;
  private Rectangle sourceRect;
  public float Rotation { get; private set; }
  public string PairedLevelName { get; private set; }

  public LockedSmallDoorBlock(Texture2D SmallDoorTexture, Vector2 xyPos, string pairedLevelName) : base(xyPos) {
    texture = SmallDoorTexture;
    Rotation = 0.0f;
    PairedLevelName = pairedLevelName;
    sourceRect = new Rectangle(448, 256, 64, 64);
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
