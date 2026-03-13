using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.WorldPickups;

public class AmmoWorldPickup : BaseWorldPickup, ICollidable {
  private Texture2D texture;
  private Rectangle sourceRectangle = new(9, 15, 12, 11);
  private Vector2 origin;
  private float scale = 2f;

  public AmmoWorldPickup(Texture2D texture, Vector2 position) : base(position) {
    this.texture = texture;
  }

  public override void Draw(SpriteBatch spriteBatch) {
    origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

    spriteBatch.Draw(
      texture,
      Position,
      sourceRectangle,
      Color.White,
      0f,
      origin,
      scale,
      SpriteEffects.None,
      0f
    );
  }

  public override void Update(GameTime gameTime) {
  }
}
