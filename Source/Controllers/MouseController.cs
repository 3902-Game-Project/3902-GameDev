using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

internal class MouseController : IController {
  // Tracking of presses / releases must be shared across GameStates
  private static readonly MouseDiffTracker mouseTracker = new();

  private readonly Dictionary<MouseButtons, ICommand> pressedMappings;
  private readonly Dictionary<MouseButtons, ICommand> downMappings;
  private readonly Dictionary<MouseButtons, ICommand> releasedMappings;

  public MouseController(
    Dictionary<MouseButtons, ICommand> pressedMappings = null,
    Dictionary<MouseButtons, ICommand> downMappings = null,
    Dictionary<MouseButtons, ICommand> releasedMappings = null
  ) {
    this.pressedMappings = pressedMappings ?? [];
    this.downMappings = downMappings ?? [];
    this.releasedMappings = releasedMappings ?? [];
  }

  public void Update(GameTime gameTime) {
    MouseState mouseState = Mouse.GetState();

    mouseTracker.Update(mouseState);

    foreach (MouseButtons mouseButton in mouseTracker.GetPressed()) {
      if (pressedMappings.TryGetValue(mouseButton, out ICommand command)) {
        command.Execute();
      }
    }

    foreach (MouseButtons mouseButton in mouseTracker.GetDown()) {
      if (downMappings.TryGetValue(mouseButton, out ICommand command)) {
        command.Execute();
      }
    }

    foreach (MouseButtons mouseButton in mouseTracker.GetReleased()) {
      if (releasedMappings.TryGetValue(mouseButton, out ICommand command)) {
        command.Execute();
      }
    }
  }
}
