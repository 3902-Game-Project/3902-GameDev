using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Interfaces;

public interface ILowLevelDrawable {
  void LowLevelDraw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch);
}
