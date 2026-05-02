using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

internal class AController(
  Dictionary<GPGamePadButtons, IGPCommand> pressedMappings = null,
  Dictionary<GPGamePadButtons, IGPCommand> downMappings = null,
  Dictionary<GPGamePadButtons, IGPCommand> releasedMappings = null
) : IController<GPGamePadButtons> {
  private static readonly PlayerIndex PLAYER_INDEX = PlayerIndex.One;

  // Tracking of presses / releases must be shared across GameStates
  private static readonly GamePadDiffTracker gamePadTracker = new();

  public Dictionary<GPGamePadButtons, IGPCommand> PressedMappings { get; } = pressedMappings ?? [];
  public Dictionary<GPGamePadButtons, IGPCommand> DownMappings { get; } = downMappings ?? [];
  public Dictionary<GPGamePadButtons, IGPCommand> ReleasedMappings { get; } = releasedMappings ?? [];

  public void Update() {
    GamePadState gamePadState = GamePad.GetState(PLAYER_INDEX);

    gamePadTracker.Update(gamePadState);

    foreach (GPGamePadButtons button in gamePadTracker.GetPressed()) {
      if (PressedMappings.TryGetValue(button, out var command)) {
        command.Execute();
      }
    }

    foreach (GPGamePadButtons button in gamePadTracker.GetDown()) {
      if (DownMappings.TryGetValue(button, out var command)) {
        command.Execute();
      }
    }

    foreach (GPGamePadButtons button in gamePadTracker.GetReleased()) {
      if (ReleasedMappings.TryGetValue(button, out var command)) {
        command.Execute();
      }
    }
  }
}
