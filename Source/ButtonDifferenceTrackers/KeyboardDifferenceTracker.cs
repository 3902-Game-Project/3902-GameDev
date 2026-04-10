using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace GameProject.ButtonDifferenceTrackers;

internal class KeyboardDifferenceTracker {
  private KeyboardState pastKeyboardState = new();
  private KeyboardState keyboardState = new();

  public void Update(KeyboardState newKeyboardState) {
    pastKeyboardState = keyboardState;
    keyboardState = newKeyboardState;
  }

  public IEnumerable<Keys> GetDown() {
    return keyboardState.GetPressedKeys();
  }

  public IEnumerable<Keys> GetPressed() {
    return keyboardState.GetPressedKeys().Where(key => !pastKeyboardState.IsKeyDown(key));
  }

  public IEnumerable<Keys> GetReleased() {
    return pastKeyboardState.GetPressedKeys().Where(key => !keyboardState.IsKeyDown(key));
  }
}
