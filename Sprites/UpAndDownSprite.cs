using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Sprites;

public class UpandDownSprite : ISprite {
  private Texture2D texture;
  private Vector2 position;
  private Vector2 startPosition;
  private Rectangle sourceRectangle;

  private int direction = -1;
  private int speed = 2;
  private int jumpHeight = 50;

  public UpandDownSprite(Texture2D texture, Vector2 position) {
    this.texture = texture;
    this.position = position;
    this.startPosition = position;

    this.sourceRectangle = new Rectangle(130, 0, 26, 31);
  }

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
