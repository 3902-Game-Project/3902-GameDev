using GameProject.Misc;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GlobalInterfaces;

internal interface ILowLevelDrawable {
  void LowLevelDraw(GraphicsDevice graphicsDevice, ValueTracker<RenderTarget2D> renderTargetTracker, SpriteBatch spriteBatch);
}
