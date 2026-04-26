using Microsoft.Xna.Framework.Input;

namespace GameProject.ButtonDiffTrackers;

internal class KeyboardDiffTracker : AButtonDiffTracker<Keys, KeyboardState> {
  public override void Update(KeyboardState keyboardState) {
    UpdateButtonState(keyboardState.GetPressedKeys());
  }
}
