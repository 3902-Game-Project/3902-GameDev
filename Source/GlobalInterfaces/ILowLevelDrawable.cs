using GameProject.Misc;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GlobalInterfaces;

internal interface ILowLevelDrawable {
  void LowLevelDraw(GraphicsDevice graphicsDevice, RenderTargetTracker renderTargetTracker, SpriteBatch spriteBatch);
}
