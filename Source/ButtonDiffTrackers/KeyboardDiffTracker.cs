using Microsoft.Xna.Framework.Input;

namespace GameProject.ButtonDiffTrackers;

internal class KeyboardDiffTracker : ButtonDiffTracker<Keys> {
  public void Update(KeyboardState keyboardState) {
    UpdateButtonState(keyboardState.GetPressedKeys());
  }
}
