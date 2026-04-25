using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Misc;

internal class RenderTargetTracker(GraphicsDevice graphicsDevice) {
  private class RenderTargetDisposer : IDisposable {
    private GraphicsDevice graphicsDevice;
    private Stack<RenderTarget2D> renderTargets;
    private bool disposedValue;

    public RenderTargetDisposer(GraphicsDevice graphicsDevice, Stack<RenderTarget2D> renderTargets, RenderTarget2D renderTarget) {
      this.graphicsDevice = graphicsDevice;
      this.renderTargets = renderTargets;

      graphicsDevice.SetRenderTarget(renderTarget);
      renderTargets.Push(renderTarget);
    }

    protected virtual void Dispose(bool disposing) {
      if (!disposedValue) {
        if (disposing) {
          // C# Docs: Dispose managed state (managed objects)
          /* no managed state to dispose, if i understand managed state correctly */
        }

        // C# Docs: Free unmanaged resources (unmanaged objects) and override finalizer
        // C# Docs: Set large fields to null
        renderTargets.Pop();
        graphicsDevice.SetRenderTarget(renderTargets.Count > 0 ? renderTargets.Peek() : null);
        disposedValue = true;
      }
    }

    // // C# Docs: Override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~RenderTargetDisposer()
    // {
    //     // C# Docs: Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose() {
      // C# Docs: Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
    }
  }
  
  private readonly Stack<RenderTarget2D> renderTargets = new();

  public IDisposable TempSetTarget(RenderTarget2D renderTarget) {
    return new RenderTargetDisposer(graphicsDevice, renderTargets, renderTarget);
  }
}
