using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

public abstract class AGamePadController : IController {
  private static readonly PlayerIndex PLAYER_INDEX = PlayerIndex.One;

  private readonly GamePadDiffTracker gamePadTracker = new();

  protected abstract Dictionary<Buttons, ICommand> PressedMappings { get; }
  protected abstract Dictionary<Buttons, ICommand> DownMappings { get; }
  protected abstract Dictionary<Buttons, ICommand> ReleasedMappings { get; }

  public void Update(GameTime gameTime) {
    GamePadState gamePadState = GamePad.GetState(PLAYER_INDEX);

    gamePadTracker.Update(gamePadState);

    foreach (Buttons button in gamePadTracker.GetPressed()) {
      if (PressedMappings.TryGetValue(button, out ICommand command)) {
        command.Execute();
      }
    }

    foreach (Buttons button in gamePadTracker.GetDown()) {
      if (DownMappings.TryGetValue(button, out ICommand command)) {
        command.Execute();
      }
    }

    foreach (Buttons button in gamePadTracker.GetReleased()) {
      if (ReleasedMappings.TryGetValue(button, out ICommand command)) {
        command.Execute();
      }
    }
  }
}
