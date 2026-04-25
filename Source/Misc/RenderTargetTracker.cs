using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Misc;

internal class RenderTargetTracker(GraphicsDevice graphicsDevice) {
  private readonly Stack<RenderTarget2D> renderTargets = new();

  public void Push(RenderTarget2D renderTarget) {
    graphicsDevice.SetRenderTarget(renderTarget);
    renderTargets.Push(renderTarget);
  }

  public void Pop() {
    renderTargets.Pop();
    graphicsDevice.SetRenderTarget(renderTargets.Count > 0 ? renderTargets.Peek() : null);
  }
}
