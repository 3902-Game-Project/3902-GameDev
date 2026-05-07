using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace GameProject.ButtonDiffTrackers;

internal enum MouseButtons {
  Left,
  Middle,
  Right,
  XButton1,
  XButton2,
};

internal record AugmentedMouseState(
  MouseState State,
  bool IsActive
);

internal class MouseDiffTracker : AButtonDiffTracker<MouseButtons, AugmentedMouseState> {
  protected override MouseButtons[] ExtractPressedFromState(AugmentedMouseState mouseState) {
    var pressedButtons = new List<MouseButtons>();

    if (mouseState.IsActive) {
      if (mouseState.State.LeftButton == ButtonState.Pressed) {
        pressedButtons.Add(MouseButtons.Left);
      }

      if (mouseState.State.MiddleButton == ButtonState.Pressed) {
        pressedButtons.Add(MouseButtons.Middle);
      }

      if (mouseState.State.RightButton == ButtonState.Pressed) {
        pressedButtons.Add(MouseButtons.Right);
      }

      if (mouseState.State.XButton1 == ButtonState.Pressed) {
        pressedButtons.Add(MouseButtons.XButton1);
      }

      if (mouseState.State.XButton2 == ButtonState.Pressed) {
        pressedButtons.Add(MouseButtons.XButton2);
      }
    }

    return [.. pressedButtons];
  }
}
