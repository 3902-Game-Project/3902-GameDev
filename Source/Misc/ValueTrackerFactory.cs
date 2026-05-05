using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Misc;

internal static class ValueTrackerFactory {
  public static ValueTracker<RenderTarget2D> CreateRenderTargetTracker(GraphicsDevice graphicsDevice) {
    return new(graphicsDevice.SetRenderTarget);
  }

  public static ValueTracker<Viewport> CreateViewportTracker(GraphicsDevice graphicsDevice, Viewport defaultViewport) {
    return new(newValue => graphicsDevice.Viewport = newValue, defaultViewport);
  }
}
