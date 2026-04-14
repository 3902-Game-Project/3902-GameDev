using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

internal class GamePadController : IController {
  private static readonly PlayerIndex PLAYER_INDEX = PlayerIndex.One;

  // Tracking of presses / releases must be shared across GameStates
  private static readonly GamePadDiffTracker gamePadTracker = new();

  private readonly Dictionary<Buttons, IGPCommand> pressedMappings;
  private readonly Dictionary<Buttons, IGPCommand> downMappings;
  private readonly Dictionary<Buttons, IGPCommand> releasedMappings;

  public GamePadController(
    Dictionary<Buttons, IGPCommand> pressedMappings = null,
    Dictionary<Buttons, IGPCommand> downMappings = null,
    Dictionary<Buttons, IGPCommand> releasedMappings = null
  ) {
    this.pressedMappings = pressedMappings ?? [];
    this.downMappings = downMappings ?? [];
    this.releasedMappings = releasedMappings ?? [];
  }

  public void Update(GameTime gameTime) {
    GamePadState gamePadState = GamePad.GetState(PLAYER_INDEX);

    gamePadTracker.Update(gamePadState);

    foreach (Buttons button in gamePadTracker.GetPressed()) {
      if (pressedMappings.TryGetValue(button, out IGPCommand command)) {
        command.Execute();
      }
    }

    foreach (Buttons button in gamePadTracker.GetDown()) {
      if (downMappings.TryGetValue(button, out IGPCommand command)) {
        command.Execute();
      }
    }

    foreach (Buttons button in gamePadTracker.GetReleased()) {
      if (releasedMappings.TryGetValue(button, out IGPCommand command)) {
        command.Execute();
      }
    }
  }
}
