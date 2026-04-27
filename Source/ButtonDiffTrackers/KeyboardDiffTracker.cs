using Microsoft.Xna.Framework.Input;

namespace GameProject.ButtonDiffTrackers;

internal class KeyboardDiffTracker : AButtonDiffTracker<Keys, KeyboardState> {
  public override Keys[] ExtractPressedFromState(KeyboardState keyboardState) {
    return keyboardState.GetPressedKeys();
  }
}
