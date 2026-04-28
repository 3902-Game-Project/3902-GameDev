using System;
using System.Collections.Generic;
using GameProject.Misc;
using Microsoft.Xna.Framework.Input;

namespace GameProject.ButtonDiffTrackers;

// Own version of enum to handle permissive vs. strict thumbstick directional inputs
internal enum GPGamePadButtons {
  A,
  B,
  Back,
  BigButton,
  DPadDown,
  DPadLeft,
  DPadRight,
  DPadUp,
  LeftShoulder,
  LeftStick,
  LeftThumbstickDown,
  LeftThumbstickDownStrict,
  LeftThumbstickLeft,
  LeftThumbstickLeftStrict,
  LeftThumbstickRight,
  LeftThumbstickRightStrict,
  LeftThumbstickUp,
  LeftThumbstickUpStrict,
  LeftTrigger,
  RightShoulder,
  RightStick,
  RightThumbstickDown,
  RightThumbstickDownStrict,
  RightThumbstickLeft,
  RightThumbstickLeftStrict,
  RightThumbstickRight,
  RightThumbstickRightStrict,
  RightThumbstickUp,
  RightThumbstickUpStrict,
  RightTrigger,
  Start,
  X,
  Y,
};

internal class GamePadDiffTracker : AButtonDiffTracker<GPGamePadButtons, GamePadState> {
  private static readonly float TRIGGER_THRESHOLD = 0.9f;
  private static readonly float STICK_THRESHOLD_SQUARED = 0.9f * 0.9f;
  // Used MathF.PI * (5.0f / 16.0f) for perfectly sized octagonal stick regions (allowing diagonals)
  private static readonly float STICK_DIAGONAL_ANGLE_THRESHOLD = MathF.PI * (5.0f / 16.0f);
  // Used MathF.PI * (1.0f / 4.0f) for cardinal directions only
  private static readonly float STICK_DIAGONAL_ANGLE_STRICT_THRESHOLD = MathF.PI * (1.0f / 4.0f);

  public override GPGamePadButtons[] ExtractPressedFromState(GamePadState gamePadState) {
    var pressedButtons = new List<GPGamePadButtons>();

    // Add buttons (in order of Buttons Enum)

    if (gamePadState.Buttons.A == ButtonState.Pressed) {
      pressedButtons.Add(GPGamePadButtons.A);
    }

    if (gamePadState.Buttons.B == ButtonState.Pressed) {
      pressedButtons.Add(GPGamePadButtons.B);
    }

    if (gamePadState.Buttons.Back == ButtonState.Pressed) {
      pressedButtons.Add(GPGamePadButtons.Back);
    }

    if (gamePadState.Buttons.BigButton == ButtonState.Pressed) {
      pressedButtons.Add(GPGamePadButtons.BigButton);
    }

    if (gamePadState.DPad.Down == ButtonState.Pressed) {
      pressedButtons.Add(GPGamePadButtons.DPadDown);
    }

    if (gamePadState.DPad.Left == ButtonState.Pressed) {
      pressedButtons.Add(GPGamePadButtons.DPadLeft);
    }

    if (gamePadState.DPad.Right == ButtonState.Pressed) {
      pressedButtons.Add(GPGamePadButtons.DPadRight);
    }

    if (gamePadState.DPad.Up == ButtonState.Pressed) {
      pressedButtons.Add(GPGamePadButtons.DPadUp);
    }

    if (gamePadState.Buttons.LeftShoulder == ButtonState.Pressed) {
      pressedButtons.Add(GPGamePadButtons.LeftShoulder);
    }

    if (gamePadState.Buttons.LeftStick == ButtonState.Pressed) {
      pressedButtons.Add(GPGamePadButtons.LeftStick);
    }

    if (gamePadState.ThumbSticks.Left.LengthSquared() > STICK_THRESHOLD_SQUARED) {
      var angle = VectorFuncs.Angle(gamePadState.ThumbSticks.Left);

      if (angle >= MathF.PI * 1.5f - STICK_DIAGONAL_ANGLE_THRESHOLD && angle <= MathF.PI * 1.5 + STICK_DIAGONAL_ANGLE_THRESHOLD) {
        pressedButtons.Add(GPGamePadButtons.LeftThumbstickDown);
      }

      if (angle >= MathF.PI - STICK_DIAGONAL_ANGLE_THRESHOLD && angle <= MathF.PI + STICK_DIAGONAL_ANGLE_THRESHOLD) {
        pressedButtons.Add(GPGamePadButtons.LeftThumbstickLeft);
      }

      if (angle >= 0.0f && angle <= STICK_DIAGONAL_ANGLE_THRESHOLD || angle >= MathF.PI * 2.0f - STICK_DIAGONAL_ANGLE_THRESHOLD && angle <= MathF.PI * 2.0f) {
        pressedButtons.Add(GPGamePadButtons.LeftThumbstickRight);
      }

      if (angle >= MathF.PI * 0.5f - STICK_DIAGONAL_ANGLE_THRESHOLD && angle <= MathF.PI * 0.5 + STICK_DIAGONAL_ANGLE_THRESHOLD) {
        pressedButtons.Add(GPGamePadButtons.LeftThumbstickUp);
      }

      if (angle >= MathF.PI * 1.5f - STICK_DIAGONAL_ANGLE_STRICT_THRESHOLD && angle <= MathF.PI * 1.5 + STICK_DIAGONAL_ANGLE_STRICT_THRESHOLD) {
        pressedButtons.Add(GPGamePadButtons.LeftThumbstickDownStrict);
      }

      if (angle >= MathF.PI - STICK_DIAGONAL_ANGLE_STRICT_THRESHOLD && angle <= MathF.PI + STICK_DIAGONAL_ANGLE_STRICT_THRESHOLD) {
        pressedButtons.Add(GPGamePadButtons.LeftThumbstickLeftStrict);
      }

      if (angle >= 0.0f && angle <= STICK_DIAGONAL_ANGLE_STRICT_THRESHOLD || angle >= MathF.PI * 2.0f - STICK_DIAGONAL_ANGLE_STRICT_THRESHOLD && angle <= MathF.PI * 2.0f) {
        pressedButtons.Add(GPGamePadButtons.LeftThumbstickRightStrict);
      }

      if (angle >= MathF.PI * 0.5f - STICK_DIAGONAL_ANGLE_STRICT_THRESHOLD && angle <= MathF.PI * 0.5 + STICK_DIAGONAL_ANGLE_STRICT_THRESHOLD) {
        pressedButtons.Add(GPGamePadButtons.LeftThumbstickUpStrict);
      }
    }

    if (gamePadState.Triggers.Left >= TRIGGER_THRESHOLD) {
      pressedButtons.Add(GPGamePadButtons.LeftTrigger);
    }

    if (gamePadState.Buttons.RightShoulder == ButtonState.Pressed) {
      pressedButtons.Add(GPGamePadButtons.RightShoulder);
    }

    if (gamePadState.Buttons.RightStick == ButtonState.Pressed) {
      pressedButtons.Add(GPGamePadButtons.RightStick);
    }

    if (gamePadState.ThumbSticks.Right.LengthSquared() > STICK_THRESHOLD_SQUARED) {
      var angle = VectorFuncs.Angle(gamePadState.ThumbSticks.Right);

      if (angle >= MathF.PI * 1.5f - STICK_DIAGONAL_ANGLE_THRESHOLD && angle <= MathF.PI * 1.5 + STICK_DIAGONAL_ANGLE_THRESHOLD) {
        pressedButtons.Add(GPGamePadButtons.RightThumbstickDown);
      }

      if (angle >= MathF.PI - STICK_DIAGONAL_ANGLE_THRESHOLD && angle <= MathF.PI + STICK_DIAGONAL_ANGLE_THRESHOLD) {
        pressedButtons.Add(GPGamePadButtons.RightThumbstickLeft);
      }

      if (angle >= 0.0f && angle <= STICK_DIAGONAL_ANGLE_THRESHOLD || angle >= MathF.PI * 2.0f - STICK_DIAGONAL_ANGLE_THRESHOLD && angle <= MathF.PI * 2.0f) {
        pressedButtons.Add(GPGamePadButtons.RightThumbstickRight);
      }

      if (angle >= MathF.PI * 0.5f - STICK_DIAGONAL_ANGLE_THRESHOLD && angle <= MathF.PI * 0.5 + STICK_DIAGONAL_ANGLE_THRESHOLD) {
        pressedButtons.Add(GPGamePadButtons.RightThumbstickUp);
      }

      if (angle >= MathF.PI * 1.5f - STICK_DIAGONAL_ANGLE_STRICT_THRESHOLD && angle <= MathF.PI * 1.5 + STICK_DIAGONAL_ANGLE_STRICT_THRESHOLD) {
        pressedButtons.Add(GPGamePadButtons.RightThumbstickDownStrict);
      }

      if (angle >= MathF.PI - STICK_DIAGONAL_ANGLE_STRICT_THRESHOLD && angle <= MathF.PI + STICK_DIAGONAL_ANGLE_STRICT_THRESHOLD) {
        pressedButtons.Add(GPGamePadButtons.RightThumbstickLeftStrict);
      }

      if (angle >= 0.0f && angle <= STICK_DIAGONAL_ANGLE_STRICT_THRESHOLD || angle >= MathF.PI * 2.0f - STICK_DIAGONAL_ANGLE_STRICT_THRESHOLD && angle <= MathF.PI * 2.0f) {
        pressedButtons.Add(GPGamePadButtons.RightThumbstickRightStrict);
      }

      if (angle >= MathF.PI * 0.5f - STICK_DIAGONAL_ANGLE_STRICT_THRESHOLD && angle <= MathF.PI * 0.5 + STICK_DIAGONAL_ANGLE_STRICT_THRESHOLD) {
        pressedButtons.Add(GPGamePadButtons.RightThumbstickUpStrict);
      }
    }

    if (gamePadState.Triggers.Right >= TRIGGER_THRESHOLD) {
      pressedButtons.Add(GPGamePadButtons.RightTrigger);
    }

    if (gamePadState.Buttons.Start == ButtonState.Pressed) {
      pressedButtons.Add(GPGamePadButtons.Start);
    }

    if (gamePadState.Buttons.X == ButtonState.Pressed) {
      pressedButtons.Add(GPGamePadButtons.X);
    }

    if (gamePadState.Buttons.Y == ButtonState.Pressed) {
      pressedButtons.Add(GPGamePadButtons.Y);
    }

    return [.. pressedButtons];
  }
}
