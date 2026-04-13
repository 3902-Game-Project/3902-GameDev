using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

public class GamePadController : IController {
  private static readonly PlayerIndex PLAYER_INDEX = PlayerIndex.One;

  private readonly GamePadDiffTracker gamePadTracker = new();

  private readonly Dictionary<Buttons, ICommand> pressedMappings;
  private readonly Dictionary<Buttons, ICommand> downMappings;
  private readonly Dictionary<Buttons, ICommand> releasedMappings;

  public GamePadController(
        Dictionary<Buttons, ICommand> pressedMappings = null,
        Dictionary<Buttons, ICommand> downMappings = null,
        Dictionary<Buttons, ICommand> releasedMappings = null) {
    this.pressedMappings = pressedMappings ?? [];
    this.downMappings = downMappings ?? [];
    this.releasedMappings = releasedMappings ?? [];
  }

  public void Update(GameTime gameTime) {
    GamePadState gamePadState = GamePad.GetState(PLAYER_INDEX);

    gamePadTracker.Update(gamePadState);

    foreach (Buttons button in gamePadTracker.GetPressed()) {
      if (pressedMappings.TryGetValue(button, out ICommand command)) {
        command.Execute();
      }
    }

    foreach (Buttons button in gamePadTracker.GetDown()) {
      if (downMappings.TryGetValue(button, out ICommand command)) {
        command.Execute();
      }
    }

    foreach (Buttons button in gamePadTracker.GetReleased()) {
      if (releasedMappings.TryGetValue(button, out ICommand command)) {
        command.Execute();
      }
    }
  }
}
