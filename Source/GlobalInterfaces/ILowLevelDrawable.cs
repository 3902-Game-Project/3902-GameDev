using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Interfaces;

internal interface ILowLevelDrawable {
  void LowLevelDraw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch);
}
