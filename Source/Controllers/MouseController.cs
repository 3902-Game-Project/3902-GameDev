using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

internal class MouseController : IController {
  // Tracking of presses / releases must be shared across GameStates
  private static readonly MouseDiffTracker mouseTracker = new();

  private readonly Dictionary<MouseButtons, IGPCommand> pressedMappings;
  private readonly Dictionary<MouseButtons, IGPCommand> downMappings;
  private readonly Dictionary<MouseButtons, IGPCommand> releasedMappings;

  public MouseController(
    Dictionary<MouseButtons, IGPCommand> pressedMappings = null,
    Dictionary<MouseButtons, IGPCommand> downMappings = null,
    Dictionary<MouseButtons, IGPCommand> releasedMappings = null
  ) {
    this.pressedMappings = pressedMappings ?? [];
    this.downMappings = downMappings ?? [];
    this.releasedMappings = releasedMappings ?? [];
  }

  public void Update(GameTime gameTime) {
    MouseState mouseState = Mouse.GetState();

    mouseTracker.Update(mouseState);

    foreach (MouseButtons mouseButton in mouseTracker.GetPressed()) {
      if (pressedMappings.TryGetValue(mouseButton, out IGPCommand command)) {
        command.Execute();
      }
    }

    foreach (MouseButtons mouseButton in mouseTracker.GetDown()) {
      if (downMappings.TryGetValue(mouseButton, out IGPCommand command)) {
        command.Execute();
      }
    }

    foreach (MouseButtons mouseButton in mouseTracker.GetReleased()) {
      if (releasedMappings.TryGetValue(mouseButton, out IGPCommand command)) {
        command.Execute();
      }
    }
  }
}
