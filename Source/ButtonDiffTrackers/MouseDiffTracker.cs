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

internal class MouseDiffTracker : AButtonDiffTracker<MouseButtons, MouseState> {
  protected override MouseButtons[] ExtractPressedFromState(MouseState mouseState) {
    var pressedButtons = new List<MouseButtons>();

    if (mouseState.LeftButton == ButtonState.Pressed) {
      pressedButtons.Add(MouseButtons.Left);
    }

    if (mouseState.MiddleButton == ButtonState.Pressed) {
      pressedButtons.Add(MouseButtons.Middle);
    }

    if (mouseState.RightButton == ButtonState.Pressed) {
      pressedButtons.Add(MouseButtons.Right);
    }

    if (mouseState.XButton1 == ButtonState.Pressed) {
      pressedButtons.Add(MouseButtons.XButton1);
    }

    if (mouseState.XButton2 == ButtonState.Pressed) {
      pressedButtons.Add(MouseButtons.XButton2);
    }

    return [.. pressedButtons];
  }
}
