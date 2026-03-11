using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Blocks;

public class OpenSmallDoorBlock : BaseBlock {
  private Texture2D texture;
  private Rectangle sourceRect;
  public float Rotation { get; private set; }

  public OpenSmallDoorBlock(Texture2D OpenSmallDoorTexture, Vector2 xyPos) : base(xyPos) {
    texture = OpenSmallDoorTexture;
    Rotation = 0.0f;
    sourceRect = new Rectangle(448, 448, 64, 64);
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
