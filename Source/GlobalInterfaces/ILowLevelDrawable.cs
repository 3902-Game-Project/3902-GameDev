using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GlobalInterfaces;

internal interface ILowLevelDrawable {
  void LowLevelDraw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, RenderTargetTracker renderTargetTracker);
}
