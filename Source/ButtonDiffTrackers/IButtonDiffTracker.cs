using System.Collections.Generic;

namespace GameProject.ButtonDiffTrackers;

internal interface IButtonDiffTracker<ButtonsEnum, ButtonStateReference> {
  IEnumerable<ButtonsEnum> GetDown();
  IEnumerable<ButtonsEnum> GetPressed();
  IEnumerable<ButtonsEnum> GetReleased();
  void Update(ButtonStateReference buttonState);
}
