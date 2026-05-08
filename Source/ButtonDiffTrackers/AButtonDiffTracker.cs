using System.Collections.Generic;
using System.Linq;

namespace GameProject.ButtonDiffTrackers;

internal abstract class AButtonDiffTracker<ButtonsEnum, ButtonStateReference> : IButtonDiffTracker<ButtonsEnum, ButtonStateReference> {
  // These 2 variables ought to be private, but MouseDiffTracker.cs has to work around hacky behavior in MonoGame itself,
  // See MouseDiffTracker.cs for explanation.
  private ButtonsEnum[] pastButtonState = [];
  private protected ButtonsEnum[] currentButtonState = [];

  private protected abstract ButtonsEnum[] ExtractPressedFromState(ButtonStateReference gamePadState);

  internal IEnumerable<ButtonsEnum> GetDown() {
    return currentButtonState;
  }

  internal IEnumerable<ButtonsEnum> GetPressed() {
    return currentButtonState.Where(button => !pastButtonState.Contains(button));
  }

  internal IEnumerable<ButtonsEnum> GetReleased() {
    return pastButtonState.Where(button => !currentButtonState.Contains(button));
  }

  internal void Update(ButtonStateReference buttonState) {
    pastButtonState = currentButtonState;
    currentButtonState = ExtractPressedFromState(buttonState);
  }
}
