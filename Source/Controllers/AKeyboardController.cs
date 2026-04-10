using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

public abstract class AKeyboardController : IController {
  private readonly KeyboardDiffTracker keyTracker = new();

  protected abstract Dictionary<Keys, ICommand> PressedMappings { get; }
  protected abstract Dictionary<Keys, ICommand> DownMappings { get; }
  protected abstract Dictionary<Keys, ICommand> ReleasedMappings { get; }

  public void Update(GameTime gameTime) {
    KeyboardState keyboardState = Keyboard.GetState();

    keyTracker.Update(keyboardState);

    foreach (Keys key in keyTracker.GetPressed()) {
      if (PressedMappings.TryGetValue(key, out ICommand command)) {
        command.Execute();
      }
    }

    foreach (Keys key in keyTracker.GetDown()) {
      if (DownMappings.TryGetValue(key, out ICommand command)) {
        command.Execute();
      }
    }

    foreach (Keys key in keyTracker.GetReleased()) {
      if (ReleasedMappings.TryGetValue(key, out ICommand command)) {
        command.Execute();
      }
    }
  }
}
