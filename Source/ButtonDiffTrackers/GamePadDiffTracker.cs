using System;
using System.Collections.Generic;
using GameProject.Misc;
using Microsoft.Xna.Framework.Input;

namespace GameProject.ButtonDiffTrackers;

internal class GamePadDiffTracker : AButtonDiffTracker<Buttons, GamePadState> {
  private static readonly float TRIGGER_THRESHOLD = 0.9f;
  private static readonly float STICK_THRESHOLD_SQUARED = 0.9f * 0.9f;
  // Use MathF.PI * (5.0f / 16.0f) for perfectly sized octagonal stick regions (allowing diagonals)
  // Use MathF.PI * (1.0f / 4.0f) for cardinal directions only
  private static readonly float STICK_DIAGONAL_ANGLE_THRESHOLD = MathF.PI * (5.0f / 16.0f);

  public override void Update(GamePadState gamePadState) {
    var pressedButtons = new List<Buttons>();

    // Add buttons (in order of Buttons Enum)

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

    if (gamePadState.ThumbSticks.Left.LengthSquared() > STICK_THRESHOLD_SQUARED) {
      var angle = VectorFuncs.Angle(gamePadState.ThumbSticks.Left);

      if (angle >= MathF.PI * 1.5f - STICK_DIAGONAL_ANGLE_THRESHOLD && angle <= MathF.PI * 1.5 + STICK_DIAGONAL_ANGLE_THRESHOLD) {
        pressedButtons.Add(Buttons.LeftThumbstickDown);
      }

      if (angle >= MathF.PI - STICK_DIAGONAL_ANGLE_THRESHOLD && angle <= MathF.PI + STICK_DIAGONAL_ANGLE_THRESHOLD) {
        pressedButtons.Add(Buttons.LeftThumbstickLeft);
      }

      if (angle >= 0.0f && angle <= STICK_DIAGONAL_ANGLE_THRESHOLD || angle >= MathF.PI * 2.0f - STICK_DIAGONAL_ANGLE_THRESHOLD && angle <= MathF.PI * 2.0f) {
        pressedButtons.Add(Buttons.LeftThumbstickRight);
      }

      if (angle >= MathF.PI * 0.5f - STICK_DIAGONAL_ANGLE_THRESHOLD && angle <= MathF.PI * 0.5 + STICK_DIAGONAL_ANGLE_THRESHOLD) {
        pressedButtons.Add(Buttons.LeftThumbstickUp);
      }
    }

    if (gamePadState.Triggers.Left >= TRIGGER_THRESHOLD) {
      pressedButtons.Add(Buttons.LeftTrigger);
    }

    if (gamePadState.Buttons.RightShoulder == ButtonState.Pressed) {
      pressedButtons.Add(Buttons.RightShoulder);
    }

    if (gamePadState.Buttons.RightStick == ButtonState.Pressed) {
      pressedButtons.Add(Buttons.RightStick);
    }

    if (gamePadState.ThumbSticks.Right.LengthSquared() > STICK_THRESHOLD_SQUARED) {
      var angle = VectorFuncs.Angle(gamePadState.ThumbSticks.Right);

      if (angle >= MathF.PI * 1.5f - STICK_DIAGONAL_ANGLE_THRESHOLD && angle <= MathF.PI * 1.5 + STICK_DIAGONAL_ANGLE_THRESHOLD) {
        pressedButtons.Add(Buttons.RightThumbstickDown);
      }

      if (angle >= MathF.PI - STICK_DIAGONAL_ANGLE_THRESHOLD && angle <= MathF.PI + STICK_DIAGONAL_ANGLE_THRESHOLD) {
        pressedButtons.Add(Buttons.RightThumbstickLeft);
      }

      if (angle >= 0.0f && angle <= STICK_DIAGONAL_ANGLE_THRESHOLD || angle >= MathF.PI * 2.0f - STICK_DIAGONAL_ANGLE_THRESHOLD && angle <= MathF.PI * 2.0f) {
        pressedButtons.Add(Buttons.RightThumbstickRight);
      }

      if (angle >= MathF.PI * 0.5f - STICK_DIAGONAL_ANGLE_THRESHOLD && angle <= MathF.PI * 0.5 + STICK_DIAGONAL_ANGLE_THRESHOLD) {
        pressedButtons.Add(Buttons.RightThumbstickUp);
      }
    }

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
