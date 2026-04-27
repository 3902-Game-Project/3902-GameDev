using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

internal class MouseController(
  Dictionary<MouseButtons, IGPCommand> pressedMappings = null,
  Dictionary<MouseButtons, IGPCommand> downMappings = null,
  Dictionary<MouseButtons, IGPCommand> releasedMappings = null
) : IController<MouseButtons> {
  // Tracking of presses / releases must be shared across GameStates
  private static readonly MouseDiffTracker mouseTracker = new();

  public Dictionary<MouseButtons, IGPCommand> PressedMappings { get; } = pressedMappings ?? [];
  public Dictionary<MouseButtons, IGPCommand> DownMappings { get; } = downMappings ?? [];
  public Dictionary<MouseButtons, IGPCommand> ReleasedMappings { get; } = releasedMappings ?? [];

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
