using System.Collections.Generic;
using GameProject;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;
using GameProject.Controllers;
using GameProject.GameStates;
using Microsoft.Xna.Framework.Input;

internal static class SavePromptControllerFactory {
  public static KeyboardController CreateKeyboardController(Game1 game, StateSavePromptType savePrompt) {
    var keyboardController = new KeyboardController(
      pressedMappings: new Dictionary<Keys, IGPCommand> {
        { Keys.A, new ExecuteSaveCommand(game, savePrompt) },
        { Keys.D, new ReturnToGameNoFadeCommand(game) },
      }
    );

    return keyboardController;
  }

  public static GamePadController CreateGamePadController(Game1 game, StateSavePromptType savePrompt) {
    // The gamepad bindings don't match the readme. this is intentional, because
    // the readme is in Xbox controller layout, but testing with a
    // nintendo pro controller seems to suggest it is pro controller layout.
    var gamePadController = new GamePadController(
      pressedMappings: new Dictionary<GPGamePadButtons, IGPCommand> {
        { GPGamePadButtons.A, new ExecuteSaveCommand(game, savePrompt) },
        { GPGamePadButtons.B, new ReturnToGameNoFadeCommand(game) },
      }
    );

    return gamePadController;
  }
}
