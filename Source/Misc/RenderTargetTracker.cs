using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Misc;

internal class RenderTargetTracker(GraphicsDevice graphicsDevice) {
  public RenderTarget2D RenderTarget { get; private set; }

  void SetRenderTarget(RenderTarget2D renderTarget) {
    graphicsDevice.SetRenderTarget(renderTarget);
    RenderTarget = renderTarget;
  }
}
