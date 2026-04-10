using Microsoft.Xna.Framework.Input;

namespace GameProject.ButtonDifferenceTrackers;

internal class KeyboardDifferenceTracker : ButtonDifferenceTracker<Keys> {
  public void Update(KeyboardState keyboardState) {
    UpdateButtonState(keyboardState.GetPressedKeys());
  }
}
