using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;

namespace GameProject.Controllers;

internal class MouseController(
  Dictionary<MouseButtons, IGPCommand> pressedMappings = null,
  Dictionary<MouseButtons, IGPCommand> downMappings = null,
  Dictionary<MouseButtons, IGPCommand> releasedMappings = null
) : AController<MouseButtons, AugmentedMouseState>(pressedMappings, downMappings, releasedMappings) {
  // Tracking of presses / releases must be shared across GameStates
  private static readonly MouseDiffTracker mouseTracker = new();

  private protected override IButtonDiffTracker<MouseButtons, AugmentedMouseState> ButtonTracker => mouseTracker;
}
