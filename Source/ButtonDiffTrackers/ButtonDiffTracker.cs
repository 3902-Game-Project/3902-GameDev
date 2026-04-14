using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace GameProject.ButtonDiffTrackers;

internal abstract class ButtonDiffTracker<ButtonsEnum> {
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

  public abstract void Update(GamePadState gamePadState);
}
