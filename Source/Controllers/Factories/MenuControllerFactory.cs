using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers.Factories;

internal static class MenuControllerFactory {
  public static KeyboardController CreateKeyboardController(Game1 game) {
    var keyboardController = new KeyboardController(
      pressedMappings: new Dictionary<Keys, IGPCommand> {
        { Keys.Q, new QuitCommand(game) },
        { Keys.Enter, new StartGameCommand(game) },
        { Keys.S, new ToggleSlowModeCommand() },
      }
    );

    return keyboardController;
  }

  public static GamePadController CreateGamePadController(Game1 game) {
    // The gamepad bindings don't match the readme. this is intentional, because
    // the readme is in Xbox controller layout, but testing with a
    // nintendo pro controller seems to suggest it is pro controller layout.
    var gamePadController = new GamePadController(
      pressedMappings: new Dictionary<GPGamePadButtons, IGPCommand> {
        { GPGamePadButtons.X, new QuitCommand(game) },
        { GPGamePadButtons.A, new StartGameCommand(game) },
        { GPGamePadButtons.Y, new ToggleSlowModeCommand() },
      }
    );

    return gamePadController;
  }
}
