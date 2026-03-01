using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Items;

public class AmmoRefill(Texture2D texture, Vector2 startPosition) : IItem {
  private Rectangle sourceRectangle = new(0, 0, 8, 8);
  private Vector2 origin;

  public void Draw(SpriteBatch spriteBatch) {
    origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

    spriteBatch.Draw(
      texture,
      startPosition,
      sourceRectangle,
      Color.White,
      0f,
      origin,
      1f,
      SpriteEffects.None,
      0f
    );
  }

  public void Update(GameTime gameTime) { }

  public void Use() {
    // Logic for using the ammo refill item
  }
}
