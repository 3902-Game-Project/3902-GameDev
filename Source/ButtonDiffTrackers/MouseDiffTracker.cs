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
  bool IsActive,
  int WindowWidth,
  int WindowHeight
);

/*
 * This class has ugly and hacky components but it is working around ugly and hacky behavior 
 * in the MonoGame mouse system itself. For mouse input specifically (not keyboard or gamepad),
 * clicking of the mouse will register even if the window is not currently in focus. Fixes online
 * suggest checking for whether Game1.IsActive is set to true (meaning the window is currently
 * in focus). However, the first mouse click outside the bounds of the window still registers
 * because Game1.IsActive is still true at the instant of the mouse click.
 */
internal class MouseDiffTracker : AButtonDiffTracker<MouseButtons, AugmentedMouseState> {
  private static bool MouseWithinWindowBounds(AugmentedMouseState mouseState) {
    return mouseState.State.X >= 0 && mouseState.State.X < mouseState.WindowWidth &&
      mouseState.State.Y >= 0 && mouseState.State.Y < mouseState.WindowHeight;
  }

  private static bool MouseCurrentlyOnWindow(AugmentedMouseState mouseState) {
    return mouseState.IsActive && MouseWithinWindowBounds(mouseState);
  }

  private protected override MouseButtons[] ExtractPressedFromState(AugmentedMouseState mouseState) {
    var pressedButtons = new List<MouseButtons>();

    if (MouseCurrentlyOnWindow(mouseState)) {
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
    } else {
      // Cannot press new buttons if mouse is not currently on top of window or game is not focused

      pressedButtons.AddRange(currentButtonState);

      if (mouseState.State.LeftButton == ButtonState.Released) {
        pressedButtons.Remove(MouseButtons.Left);
      }

      if (mouseState.State.MiddleButton == ButtonState.Released) {
        pressedButtons.Remove(MouseButtons.Middle);
      }

      if (mouseState.State.RightButton == ButtonState.Released) {
        pressedButtons.Remove(MouseButtons.Right);
      }

      if (mouseState.State.XButton1 == ButtonState.Released) {
        pressedButtons.Remove(MouseButtons.XButton1);
      }

      if (mouseState.State.XButton2 == ButtonState.Released) {
        pressedButtons.Remove(MouseButtons.XButton2);
      }
    }

    return [.. pressedButtons];
  }
}
