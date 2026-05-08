using Microsoft.Xna.Framework.Input;

namespace GameProject.ButtonDiffTrackers;

internal class KeyboardDiffTracker : AButtonDiffTracker<Keys, KeyboardState> {
  private protected override Keys[] ExtractPressedFromState(KeyboardState keyboardState) {
    return keyboardState.GetPressedKeys();
  }
}
