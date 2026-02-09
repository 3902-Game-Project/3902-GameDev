using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Sprites;

public class EmptySprite : ISprite {
  public void Update(GameTime gameTime) { }

  public void Draw(SpriteBatch spriteBatch) { }
}
