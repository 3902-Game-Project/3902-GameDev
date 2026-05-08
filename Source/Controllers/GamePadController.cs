using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

internal class GamePadController(
  Dictionary<GPGamePadButtons, IGPCommand> pressedMappings = null,
  Dictionary<GPGamePadButtons, IGPCommand> downMappings = null,
  Dictionary<GPGamePadButtons, IGPCommand> releasedMappings = null
) : AController<GPGamePadButtons, GamePadState>(pressedMappings, downMappings, releasedMappings) {
  // Tracking of presses / releases must be shared across GameStates
  private static readonly GamePadDiffTracker gamePadTracker = new();

  protected override IButtonDiffTracker<GPGamePadButtons, GamePadState> ButtonTracker => gamePadTracker;
}
