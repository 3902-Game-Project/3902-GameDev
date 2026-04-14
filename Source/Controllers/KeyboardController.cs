using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

internal class KeyboardController : IController {
  // Tracking of presses / releases must be shared across GameStates
  private static readonly KeyboardDiffTracker keyTracker = new();

  private readonly Dictionary<Keys, ICommand> pressedMappings;
  private readonly Dictionary<Keys, ICommand> downMappings;
  private readonly Dictionary<Keys, ICommand> releasedMappings;

  public KeyboardController(
    Dictionary<Keys, ICommand> pressedMappings = null,
    Dictionary<Keys, ICommand> downMappings = null,
    Dictionary<Keys, ICommand> releasedMappings = null
  ) {
    this.pressedMappings = pressedMappings ?? [];
    this.downMappings = downMappings ?? [];
    this.releasedMappings = releasedMappings ?? [];
  }

  public void Update(GameTime gameTime) {
    KeyboardState keyboardState = Keyboard.GetState();

    keyTracker.Update(keyboardState);

    foreach (Keys key in keyTracker.GetPressed()) {
      if (pressedMappings.TryGetValue(key, out ICommand command)) {
        command.Execute();
      }
    }

    foreach (Keys key in keyTracker.GetDown()) {
      if (downMappings.TryGetValue(key, out ICommand command)) {
        command.Execute();
      }
    }

    foreach (Keys key in keyTracker.GetReleased()) {
      if (releasedMappings.TryGetValue(key, out ICommand command)) {
        command.Execute();
      }
    }
  }
}
