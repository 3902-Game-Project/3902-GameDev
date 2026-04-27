using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

internal class GamePadController : IController<Buttons> {
  private static readonly PlayerIndex PLAYER_INDEX = PlayerIndex.One;

  // Tracking of presses / releases must be shared across GameStates
  private static readonly GamePadDiffTracker gamePadTracker = new();

  public Dictionary<Buttons, IGPCommand> PressedMappings { get; }
  public Dictionary<Buttons, IGPCommand> DownMappings { get; }
  public Dictionary<Buttons, IGPCommand> ReleasedMappings { get; }

  public GamePadController(
    Dictionary<Buttons, IGPCommand> pressedMappings = null,
    Dictionary<Buttons, IGPCommand> downMappings = null,
    Dictionary<Buttons, IGPCommand> releasedMappings = null
  ) {
    PressedMappings = pressedMappings ?? [];
    DownMappings = downMappings ?? [];
    ReleasedMappings = releasedMappings ?? [];
  }

  public void Update() {
    GamePadState gamePadState = GamePad.GetState(PLAYER_INDEX);

    gamePadTracker.Update(gamePadState);

    foreach (Buttons button in gamePadTracker.GetPressed()) {
      if (PressedMappings.TryGetValue(button, out var command)) {
        command.Execute();
      }
    }

    foreach (Buttons button in gamePadTracker.GetDown()) {
      if (DownMappings.TryGetValue(button, out var command)) {
        command.Execute();
      }
    }

    foreach (Buttons button in gamePadTracker.GetReleased()) {
      if (ReleasedMappings.TryGetValue(button, out var command)) {
        command.Execute();
      }
    }
  }
}
