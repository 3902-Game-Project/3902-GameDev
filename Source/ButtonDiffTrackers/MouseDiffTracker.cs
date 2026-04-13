using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace GameProject.ButtonDiffTrackers;

public enum MouseButtons {
  Left,
  Middle,
  Right,
  XButton1,
  XButton2,
};

internal class MouseDiffTracker : ButtonDiffTracker<MouseButtons> {
  public void Update(MouseState mouseState) {
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

    UpdateButtonState([.. pressedButtons]);
  }
}
