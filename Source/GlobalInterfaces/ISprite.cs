using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Interfaces;

internal interface ISprite {
  public void Update(GameTime gameTime);
  public void Draw(SpriteBatch spriteBatch);
}
