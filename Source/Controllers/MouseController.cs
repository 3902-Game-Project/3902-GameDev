using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

internal class MouseController : IController<MouseButtons> {
  // Tracking of presses / releases must be shared across GameStates
  private static readonly MouseDiffTracker mouseTracker = new();

  public Dictionary<MouseButtons, IGPCommand> PressedMappings { get; }
  public Dictionary<MouseButtons, IGPCommand> DownMappings { get; }
  public Dictionary<MouseButtons, IGPCommand> ReleasedMappings { get; }

  public MouseController(
    Dictionary<MouseButtons, IGPCommand> pressedMappings = null,
    Dictionary<MouseButtons, IGPCommand> downMappings = null,
    Dictionary<MouseButtons, IGPCommand> releasedMappings = null
  ) {
    PressedMappings = pressedMappings ?? [];
    DownMappings = downMappings ?? [];
    ReleasedMappings = releasedMappings ?? [];
  }

  public void Update() {
    MouseState mouseState = Mouse.GetState();

    mouseTracker.Update(mouseState);

    foreach (MouseButtons mouseButton in mouseTracker.GetPressed()) {
      if (PressedMappings.TryGetValue(mouseButton, out var command)) {
        command.Execute();
      }
    }

    foreach (MouseButtons mouseButton in mouseTracker.GetDown()) {
      if (DownMappings.TryGetValue(mouseButton, out var command)) {
        command.Execute();
      }
    }

    foreach (MouseButtons mouseButton in mouseTracker.GetReleased()) {
      if (ReleasedMappings.TryGetValue(mouseButton, out var command)) {
        command.Execute();
      }
    }
  }
}
