using System;
using System.Collections.Generic;
using GameProject.HelperFuncs;
using Microsoft.Xna.Framework;
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
  private static readonly float STICK_THRESHOLD = 0.6f;
  private static readonly float STICK_THRESHOLD_SQUARED = STICK_THRESHOLD * STICK_THRESHOLD;
  // Used MathF.PI * (5.0f / 16.0f) for perfectly sized octagonal stick regions (allowing diagonals)
  private static readonly float STICK_DIAGONAL_ANGLE_THRESHOLD = MathF.PI * (5.0f / 16.0f);
  // Used MathF.PI * (1.0f / 4.0f) for cardinal directions only
  private static readonly float STICK_DIAGONAL_ANGLE_STRICT_THRESHOLD = MathF.PI * (1.0f / 4.0f);

  private static void AddStickDirectionButtons(
    List<GPGamePadButtons> pressedButtons,
    Vector2 stickPosition,
    float angleThreshold,
    GPGamePadButtons upEnum,
    GPGamePadButtons downEnum,
    GPGamePadButtons leftEnum,
    GPGamePadButtons rightEnum
  ) {
    if (stickPosition.LengthSquared() > STICK_THRESHOLD_SQUARED) {
      var angle = VectorFuncs.Angle(stickPosition);

      if (angle >= -angleThreshold && angle <= angleThreshold) {
        pressedButtons.Add(rightEnum);
      }

      if (angle >= MathF.PI * 0.5f - angleThreshold && angle <= MathF.PI * 0.5f + angleThreshold) {
        pressedButtons.Add(upEnum);
      }

      if (angle >= MathF.PI - angleThreshold && angle <= MathF.PI || angle >= MathF.PI * -1.0f && angle <= MathF.PI * -1.0f + angleThreshold) {
        pressedButtons.Add(leftEnum);
      }

      if (angle >= MathF.PI * -0.5f - angleThreshold && angle <= MathF.PI * -0.5f + angleThreshold) {
        pressedButtons.Add(downEnum);
      }
    }
  }

  protected override GPGamePadButtons[] ExtractPressedFromState(GamePadState gamePadState) {
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

    AddStickDirectionButtons(
      pressedButtons: pressedButtons,
      stickPosition: gamePadState.ThumbSticks.Left,
      angleThreshold: STICK_DIAGONAL_ANGLE_THRESHOLD,
      upEnum: GPGamePadButtons.LeftThumbstickUp,
      downEnum: GPGamePadButtons.LeftThumbstickDown,
      leftEnum: GPGamePadButtons.LeftThumbstickLeft,
      rightEnum: GPGamePadButtons.LeftThumbstickRight
    );

    AddStickDirectionButtons(
      pressedButtons: pressedButtons,
      stickPosition: gamePadState.ThumbSticks.Left,
      angleThreshold: STICK_DIAGONAL_ANGLE_STRICT_THRESHOLD,
      upEnum: GPGamePadButtons.LeftThumbstickUpStrict,
      downEnum: GPGamePadButtons.LeftThumbstickDownStrict,
      leftEnum: GPGamePadButtons.LeftThumbstickLeftStrict,
      rightEnum: GPGamePadButtons.LeftThumbstickRightStrict
    );

    if (gamePadState.Triggers.Left >= TRIGGER_THRESHOLD) {
      pressedButtons.Add(GPGamePadButtons.LeftTrigger);
    }

    if (gamePadState.Buttons.RightShoulder == ButtonState.Pressed) {
      pressedButtons.Add(GPGamePadButtons.RightShoulder);
    }

    if (gamePadState.Buttons.RightStick == ButtonState.Pressed) {
      pressedButtons.Add(GPGamePadButtons.RightStick);
    }

    AddStickDirectionButtons(
      pressedButtons: pressedButtons,
      stickPosition: gamePadState.ThumbSticks.Right,
      angleThreshold: STICK_DIAGONAL_ANGLE_THRESHOLD,
      upEnum: GPGamePadButtons.RightThumbstickUp,
      downEnum: GPGamePadButtons.RightThumbstickDown,
      leftEnum: GPGamePadButtons.RightThumbstickLeft,
      rightEnum: GPGamePadButtons.RightThumbstickRight
    );

    AddStickDirectionButtons(
      pressedButtons: pressedButtons,
      stickPosition: gamePadState.ThumbSticks.Right,
      angleThreshold: STICK_DIAGONAL_ANGLE_STRICT_THRESHOLD,
      upEnum: GPGamePadButtons.RightThumbstickUpStrict,
      downEnum: GPGamePadButtons.RightThumbstickDownStrict,
      leftEnum: GPGamePadButtons.RightThumbstickLeftStrict,
      rightEnum: GPGamePadButtons.RightThumbstickRightStrict
    );

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
