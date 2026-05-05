using GameProject.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GlobalInterfaces;

internal delegate void ClearWindowCallback(Color color);

internal record LowLevelDrawParams(
  ClearWindowCallback ClearWindowCallback,
  ValueTracker<RenderTarget2D> RenderTargetTracker,
  ValueTracker<Viewport> ViewportTracker,
  SpriteBatch SpriteBatch
);

internal interface ILowLevelDrawable {
  void LowLevelDraw(LowLevelDrawParams drawData);
}
