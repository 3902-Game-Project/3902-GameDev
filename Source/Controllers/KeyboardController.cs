using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;
using GameProject.Source.Misc;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

internal class KeyboardController : IController<Keys> {
  // Tracking of presses / releases must be shared across GameStates
  private static readonly KeyboardDiffTracker keyTracker = new();

  public Dictionary<Keys, IGPCommand> PressedMappings { get; }
  public Dictionary<Keys, IGPCommand> DownMappings { get; }
  public Dictionary<Keys, IGPCommand> ReleasedMappings { get; }

  public KeyboardController(
    Dictionary<Keys, IGPCommand> pressedMappings = null,
    Dictionary<Keys, IGPCommand> downMappings = null,
    Dictionary<Keys, IGPCommand> releasedMappings = null
  ) {
    PressedMappings = pressedMappings ?? [];
    DownMappings = downMappings ?? [];
    ReleasedMappings = releasedMappings ?? [];
  }

  public void Update() {
    KeyboardState keyboardState = Keyboard.GetState();

    keyTracker.Update(keyboardState);

    foreach (Keys key in keyTracker.GetPressed()) {
      CheatCodes.Instance.AddKey(key);
      if (PressedMappings.TryGetValue(key, out var command)) {
        command.Execute();
      }
    }

    foreach (Keys key in keyTracker.GetDown()) {
      if (DownMappings.TryGetValue(key, out var command)) {
        command.Execute();
      }
    }

    foreach (Keys key in keyTracker.GetReleased()) {
      if (ReleasedMappings.TryGetValue(key, out var command)) {
        command.Execute();
      }
    }
  }
}
