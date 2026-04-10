using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace GameProject.ButtonDifferenceTrackers;

internal class GamePadDifferenceTracker : ButtonDifferenceTracker<Buttons> {
  private static float TRIGGER_THRESHOLD = 0.9f;

  public void Update(GamePadState gamePadState) {
    var pressedButtons = new List<Buttons>();

    if (gamePadState.Buttons.A == ButtonState.Pressed) {
      pressedButtons.Add(Buttons.A);
    }

    if (gamePadState.Buttons.B == ButtonState.Pressed) {
      pressedButtons.Add(Buttons.B);
    }

    if (gamePadState.Buttons.Back == ButtonState.Pressed) {
      pressedButtons.Add(Buttons.Back);
    }

    if (gamePadState.Buttons.BigButton == ButtonState.Pressed) {
      pressedButtons.Add(Buttons.BigButton);
    }

    if (gamePadState.DPad.Down == ButtonState.Pressed) {
      pressedButtons.Add(Buttons.DPadDown);
    }

    if (gamePadState.DPad.Left == ButtonState.Pressed) {
      pressedButtons.Add(Buttons.DPadLeft);
    }

    if (gamePadState.DPad.Right == ButtonState.Pressed) {
      pressedButtons.Add(Buttons.DPadRight);
    }

    if (gamePadState.DPad.Up == ButtonState.Pressed) {
      pressedButtons.Add(Buttons.DPadUp);
    }

    if (gamePadState.Buttons.LeftShoulder == ButtonState.Pressed) {
      pressedButtons.Add(Buttons.LeftShoulder);
    }

    if (gamePadState.Buttons.LeftStick == ButtonState.Pressed) {
      pressedButtons.Add(Buttons.LeftStick);
    }

    /* Omitted left thumbstick direction buttons */

    if (gamePadState.Triggers.Left >= TRIGGER_THRESHOLD) {
      pressedButtons.Add(Buttons.LeftTrigger);
    }

    if (gamePadState.Buttons.RightShoulder == ButtonState.Pressed) {
      pressedButtons.Add(Buttons.RightShoulder);
    }

    if (gamePadState.Buttons.RightStick == ButtonState.Pressed) {
      pressedButtons.Add(Buttons.RightStick);
    }

    /* Omitted right thumbstick direction buttons */

    if (gamePadState.Triggers.Right >= TRIGGER_THRESHOLD) {
      pressedButtons.Add(Buttons.RightTrigger);
    }

    if (gamePadState.Buttons.Start == ButtonState.Pressed) {
      pressedButtons.Add(Buttons.Start);
    }

    if (gamePadState.Buttons.X == ButtonState.Pressed) {
      pressedButtons.Add(Buttons.X);
    }

    if (gamePadState.Buttons.Y == ButtonState.Pressed) {
      pressedButtons.Add(Buttons.Y);
    }

    UpdateButtonState([.. pressedButtons]);
  }
}
