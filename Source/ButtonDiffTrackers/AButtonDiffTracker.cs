using System.Collections.Generic;
using System.Linq;

namespace GameProject.ButtonDiffTrackers;

internal abstract class AButtonDiffTracker<ButtonsEnum, ButtonStateReference> : IButtonDiffTracker<ButtonsEnum, ButtonStateReference> {
  private ButtonsEnum[] pastButtonState = [];
  private ButtonsEnum[] currentButtonState = [];

  protected abstract ButtonsEnum[] ExtractPressedFromState(ButtonStateReference gamePadState, bool isActive);

  public IEnumerable<ButtonsEnum> GetDown() {
    return currentButtonState;
  }

  public IEnumerable<ButtonsEnum> GetPressed() {
    return currentButtonState.Where(button => !pastButtonState.Contains(button));
  }

  public IEnumerable<ButtonsEnum> GetReleased() {
    return pastButtonState.Where(button => !currentButtonState.Contains(button));
  }

  public void Update(ButtonStateReference buttonState, bool isActive) {
    pastButtonState = currentButtonState;
    currentButtonState = ExtractPressedFromState(buttonState, isActive);
  }
}
