using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Misc;

internal static class ValueTrackerFactory {
  public static ValueTracker<RenderTarget2D> CreateRenderTargetTracker(GraphicsDevice graphicsDevice) {
    return new(newValue => graphicsDevice.SetRenderTarget(newValue));
  }
}
