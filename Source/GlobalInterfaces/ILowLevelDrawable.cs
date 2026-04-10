using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Interfaces;

public interface ILowLevelDrawable {
  void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch);
}
