using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

public abstract class AMouseController : IController {
  private readonly MouseDiffTracker mouseTracker = new();

  protected abstract Dictionary<MouseButtons, ICommand> PressedMappings { get; }
  protected abstract Dictionary<MouseButtons, ICommand> DownMappings { get; }
  protected abstract Dictionary<MouseButtons, ICommand> ReleasedMappings { get; }

  public void Update(GameTime gameTime) {
    MouseState mouseState = Mouse.GetState();

    mouseTracker.Update(mouseState);

    foreach (MouseButtons mouseButton in mouseTracker.GetPressed()) {
      if (PressedMappings.TryGetValue(mouseButton, out ICommand command)) {
        command.Execute();
      }
    }

    foreach (MouseButtons mouseButton in mouseTracker.GetDown()) {
      if (DownMappings.TryGetValue(mouseButton, out ICommand command)) {
        command.Execute();
      }
    }

    foreach (MouseButtons mouseButton in mouseTracker.GetReleased()) {
      if (ReleasedMappings.TryGetValue(mouseButton, out ICommand command)) {
        command.Execute();
      }
    }
  }
}
