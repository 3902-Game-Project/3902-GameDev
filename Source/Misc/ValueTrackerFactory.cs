using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Misc;

internal static class ValueTrackerFactory {
  internal static ValueTracker<RenderTarget2D> CreateRenderTargetTracker(GraphicsDevice graphicsDevice) {
    return new(graphicsDevice.SetRenderTarget);
  }

  internal static ValueTracker<Viewport> CreateViewportTracker(GraphicsDevice graphicsDevice, Viewport defaultViewport) {
    return new(newValue => graphicsDevice.Viewport = newValue, defaultViewport);
  }
}
