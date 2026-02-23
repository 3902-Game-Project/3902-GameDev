using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Sprites;

public class FixedSprite(Texture2D texture, Vector2 position) : ISprite {
  public void Update(GameTime gameTime) {
  }

  public void Draw(SpriteBatch spriteBatch) {
    Rectangle sourceRect = new(0, 0, 22, 30);
    spriteBatch.Draw(texture, position, sourceRect, Color.White);
  }
}
