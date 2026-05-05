using GameProject.Misc;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GlobalInterfaces;

internal record LowLevelDrawParams (
  GraphicsDevice GraphicsDevice,
  ValueTracker<RenderTarget2D> RenderTargetTracker,
  SpriteBatch SpriteBatch
);

internal interface ILowLevelDrawable {
  void LowLevelDraw(LowLevelDrawParams drawData);
}
