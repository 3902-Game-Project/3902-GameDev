using Microsoft.Xna.Framework.Input;

namespace GameProject.ButtonDiffTrackers;

internal class KeyboardDiffTracker : ButtonDiffTracker<Keys, KeyboardState> {
  public override void Update(KeyboardState keyboardState) {
    UpdateButtonState(keyboardState.GetPressedKeys());
  }
}
