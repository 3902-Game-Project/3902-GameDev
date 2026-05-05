using System;
using System.Collections.Generic;

namespace GameProject.Misc;

#nullable enable

internal delegate void SetValueCallback<T>(T? newValue);

internal class ValueTracker<T>(SetValueCallback<T> setValueCallback, T? defaultValue = default) {
  private class ValueDisposer : IDisposable {
    private readonly SetValueCallback<T> setValueCallback;
    private readonly Stack<T> values;
    private readonly T? defaultValue;
    private bool disposedValue;

    public ValueDisposer(SetValueCallback<T> setValueCallback, Stack<T> values, T? defaultValue, T newValue) {
      this.setValueCallback = setValueCallback;
      this.values = values;
      this.defaultValue = defaultValue;

      setValueCallback(newValue);
      values.Push(newValue);
    }

    protected virtual void Dispose(bool disposing) {
      if (!disposedValue) {
        if (disposing) {
          // C# Docs: Dispose managed state (managed objects)
          /* no managed state to dispose, if i understand managed state correctly */
        }

        // C# Docs: Free unmanaged resources (unmanaged objects) and override finalizer
        // C# Docs: Set large fields to null
        values.Pop();
        setValueCallback(values.Count > 0 ? values.Peek() : defaultValue);
        disposedValue = true;
      }
    }

    // // C# Docs: Override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~ValueDisposer()
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

  private readonly Stack<T> values = new();

  public IDisposable TempSet(T newValue) {
    return new ValueDisposer(setValueCallback, values, defaultValue, newValue);
  }
}
