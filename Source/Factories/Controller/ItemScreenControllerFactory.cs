using System.Collections.Generic;
using GameProject;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;
using GameProject.Controllers;
using GameProject.GameStates;
using GameProject.Globals;
using GameProject.Managers;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework.Input;

internal static class ItemScreenControllerFactory {
  public static KeyboardController CreateKeyboardController(Game1 game, StateItemScreenType itemScreen, Player player) {
    var keyboardController = new KeyboardController(
      pressedMappings: new Dictionary<Keys, IGPCommand> {
        { Keys.Q, new QuitCommand(game) },
        { Keys.I, new ReturnToGameNoFadeCommand(game) },
                
        // Navigation bindings
        { Keys.W, new MenuMoveUpCommand(itemScreen) },
        { Keys.Up, new MenuMoveUpCommand(itemScreen) },
        { Keys.S, new MenuMoveDownCommand(itemScreen) },
        { Keys.Down, new MenuMoveDownCommand(itemScreen) },
        { Keys.A, new MenuMoveLeftCommand(itemScreen) },
        { Keys.Left, new MenuMoveLeftCommand(itemScreen) },
        { Keys.D, new MenuMoveRightCommand(itemScreen) },
        { Keys.Right, new MenuMoveRightCommand(itemScreen) },
                
        // Action bindings
        { Keys.Enter, new MenuEquipCommand(itemScreen) },
        { Keys.Space, new MenuEquipCommand(itemScreen) },
        { Keys.C, new PlayerDropItemCommand(player) },
      }
    );

    return keyboardController;
  }

  public static GamePadController CreateGamePadController(Game1 game, StateItemScreenType itemScreen, Player player) {
    // The gamepad bindings don't match the readme. this is intentional, because
    // the readme is in Xbox controller layout, but testing with a
    // nintendo pro controller seems to suggest it is pro controller layout.
    var gamePadController = new GamePadController(
      pressedMappings: new Dictionary<GPGamePadButtons, IGPCommand> {
        { GPGamePadButtons.X, new QuitCommand(game) },
        { GPGamePadButtons.B, new ReturnToGameNoFadeCommand(game) },
                
        // Navigation bindings
        { GPGamePadButtons.DPadUp, new MenuMoveUpCommand(itemScreen) },
        { GPGamePadButtons.DPadDown, new MenuMoveDownCommand(itemScreen) },
        { GPGamePadButtons.DPadLeft, new MenuMoveLeftCommand(itemScreen) },
        { GPGamePadButtons.DPadRight, new MenuMoveRightCommand(itemScreen) },
                
        // Action bindings
        { GPGamePadButtons.A, new MenuEquipCommand(itemScreen) },
        { GPGamePadButtons.Y, new PlayerDropItemCommand(player) },
      }
    );

    return gamePadController;
  }
}
