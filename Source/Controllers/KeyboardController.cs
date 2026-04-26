using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;
using GameProject.Source.Misc;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

internal class KeyboardController(
  Dictionary<Keys, IGPCommand> pressedMappings = null,
  Dictionary<Keys, IGPCommand> downMappings = null,
  Dictionary<Keys, IGPCommand> releasedMappings = null
) : IController {
  // Tracking of presses / releases must be shared across GameStates
  private static readonly KeyboardDiffTracker keyTracker = new();

  private readonly Dictionary<Keys, IGPCommand> pressedMappings = pressedMappings ?? [];
  private readonly Dictionary<Keys, IGPCommand> downMappings = downMappings ?? [];
  private readonly Dictionary<Keys, IGPCommand> releasedMappings = releasedMappings ?? [];

  public void Update() {
    KeyboardState keyboardState = Keyboard.GetState();

    keyTracker.Update(keyboardState);

    foreach (Keys key in keyTracker.GetPressed()) {
      CheatCodes.Instance.AddKey(key);
      if (pressedMappings.TryGetValue(key, out var command)) {
        command.Execute();
      }
    }

    foreach (Keys key in keyTracker.GetDown()) {
      if (downMappings.TryGetValue(key, out var command)) {
        command.Execute();
      }
    }

    foreach (Keys key in keyTracker.GetReleased()) {
      if (releasedMappings.TryGetValue(key, out var command)) {
        command.Execute();
      }
    }
  }
}
