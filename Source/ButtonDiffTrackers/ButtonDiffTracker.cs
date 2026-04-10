using System.Collections.Generic;
using System.Linq;

namespace GameProject.ButtonDiffTrackers;

internal class ButtonDiffTracker<ButtonsEnum> {
  private ButtonsEnum[] pastButtonState = [];
  private ButtonsEnum[] currentButtonState = [];

  protected void UpdateButtonState(ButtonsEnum[] newButtonState) {
    pastButtonState = currentButtonState;
    currentButtonState = newButtonState;
  }

  public IEnumerable<ButtonsEnum> GetDown() {
    return currentButtonState;
  }

  public IEnumerable<ButtonsEnum> GetPressed() {
    return currentButtonState.Where(button => !pastButtonState.Contains(button));
  }

  public IEnumerable<ButtonsEnum> GetReleased() {
    return pastButtonState.Where(button => !currentButtonState.Contains(button));
  }
}
