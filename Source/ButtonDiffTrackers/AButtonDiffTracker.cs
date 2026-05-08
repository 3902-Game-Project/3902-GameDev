using System.Collections.Generic;
using System.Linq;

namespace GameProject.ButtonDiffTrackers;

internal abstract class AButtonDiffTracker<ButtonsEnum, ButtonStateReference> : IButtonDiffTracker<ButtonsEnum, ButtonStateReference> {
  // These 2 variables ought to be private, but MouseDiffTracker.cs has to work around hacky behavior in MonoGame itself,
  // See MouseDiffTracker.cs for explanation.
  private ButtonsEnum[] pastButtonState = [];
  protected ButtonsEnum[] currentButtonState = [];

  protected abstract ButtonsEnum[] ExtractPressedFromState(ButtonStateReference gamePadState);

  public IEnumerable<ButtonsEnum> GetDown() {
    return currentButtonState;
  }

  public IEnumerable<ButtonsEnum> GetPressed() {
    return currentButtonState.Where(button => !pastButtonState.Contains(button));
  }

  public IEnumerable<ButtonsEnum> GetReleased() {
    return pastButtonState.Where(button => !currentButtonState.Contains(button));
  }

  public void Update(ButtonStateReference buttonState) {
    pastButtonState = currentButtonState;
    currentButtonState = ExtractPressedFromState(buttonState);
  }
}
