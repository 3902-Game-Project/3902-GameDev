using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

internal class MouseController(
  Dictionary<MouseButtons, IGPCommand> pressedMappings = null,
  Dictionary<MouseButtons, IGPCommand> downMappings = null,
  Dictionary<MouseButtons, IGPCommand> releasedMappings = null
) : AController<MouseButtons, MouseState>(pressedMappings, downMappings, releasedMappings) {
  // Tracking of presses / releases must be shared across GameStates
  private static readonly MouseDiffTracker mouseTracker = new();

  protected override IButtonDiffTracker<MouseButtons, MouseState> ButtonTracker => mouseTracker;

  protected override MouseState GetButtonState() => Mouse.GetState();
}
