using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Interfaces;

public interface ISprite {
  public void Update(GameTime gameTime);
  public void Draw(SpriteBatch spriteBatch);
}
