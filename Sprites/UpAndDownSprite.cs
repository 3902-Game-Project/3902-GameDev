using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Sprites;

public class UpAndDownSprite(Texture2D texture, Vector2 startPosition) : ISprite {
  private readonly Vector2 startPosition = startPosition;
  private Vector2 position = startPosition;
  private Rectangle sourceRectangle = new Rectangle(130, 0, 26, 31);

  private int direction = -1;
  private int speed = 2;
  private int jumpHeight = 50;

  public void Update(GameTime gameTime) {
    position.Y += speed * direction;

    if (direction == -1 && position.Y <= startPosition.Y - jumpHeight) {
      direction = 1;
    } else if (direction == 1 && position.Y >= startPosition.Y) {
      position.Y = startPosition.Y;
      direction = -1;
    }
  }

  public void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, position, sourceRectangle, Color.White);
  }
}
