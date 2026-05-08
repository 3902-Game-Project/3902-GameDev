using System.Collections.Generic;

namespace GameProject.ButtonDiffTrackers;

internal interface IButtonDiffTracker<ButtonsEnum, ButtonStateReference> {
  internal IEnumerable<ButtonsEnum> GetDown();
  internal IEnumerable<ButtonsEnum> GetPressed();
  internal IEnumerable<ButtonsEnum> GetReleased();
  internal void Update(ButtonStateReference buttonState);
}
