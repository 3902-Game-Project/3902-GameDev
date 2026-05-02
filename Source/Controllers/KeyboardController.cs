using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;
using GameProject.Source.Misc;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

internal class KeyboardController(
  Dictionary<Keys, IGPCommand> pressedMappings = null,
  Dictionary<Keys, IGPCommand> downMappings = null,
  Dictionary<Keys, IGPCommand> releasedMappings = null
) : AController<Keys, KeyboardState>(pressedMappings, downMappings, releasedMappings) {
  // Tracking of presses / releases must be shared across GameStates
  private static readonly KeyboardDiffTracker keyTracker = new();

  protected override IButtonDiffTracker<Keys, KeyboardState> ButtonTracker => keyTracker;

  protected override KeyboardState GetButtonState() => Keyboard.GetState();
}
