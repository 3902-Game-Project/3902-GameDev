using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;
using GameProject.Controllers;
using GameProject.Globals;
using GameProject.Level;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Factories.Controller;

internal static class GameControllerFactory {
  public static KeyboardController CreateKeyboardController(Game1 game, Player player, ILevelManager levelManager) {
    var keyboardController = new KeyboardController(
      pressedMappings: new Dictionary<Keys, IGPCommand> {
        { Keys.Q, new QuitCommand(game) },
        { Keys.Back, new ReturnToMenuAndResetCommand(game) },
        { Keys.P, new PauseCommand(game) },
        { Keys.I, new OpenItemScreenCommand(game) },
        { Keys.J, new PlayerUseItemCommand(player, UseType.Pressed) },
        { Keys.K, new PlayerUseKeyCommand(player, UseType.Pressed) },
        { Keys.F, new PlayerInteractCommand(player) },
        { Keys.Space, new PlayerSwapWeaponCommand(player) },
        { Keys.C, new PlayerDropItemCommand(game.StateGame.Player) },
        { Keys.R, new PlayerReloadWeaponCommand(player) },
        { Keys.L, new PlayerDieCommand(player) },
        { Keys.N, new OpenSavePromptCommand(game) },
        { Keys.M, new OpenLoadPromptCommand(game) },
        { Keys.Tab, new ToggleMusicCommand() },
      },
      downMappings: new Dictionary<Keys, IGPCommand> {
        { Keys.W, new PlayerMoveUpCommand(player) },
        { Keys.S, new PlayerMoveDownCommand(player) },
        { Keys.A, new PlayerMoveLeftCommand(player) },
        { Keys.D, new PlayerMoveRightCommand(player) },
        { Keys.Up, new PlayerMoveUpCommand(player) },
        { Keys.Down, new PlayerMoveDownCommand(player) },
        { Keys.Left, new PlayerMoveLeftCommand(player) },
        { Keys.Right, new PlayerMoveRightCommand(player) },
        { Keys.J, new PlayerUseItemCommand(player, UseType.Held) },
      },
      releasedMappings: new Dictionary<Keys, IGPCommand> {
        { Keys.J, new PlayerUseItemCommand(player, UseType.Released) },
      }
    );

    // Debug button binds:
    if (Flags.DebugButtonBinds) {
      keyboardController.PressedMappings.Add(Keys.T, new PreviousLevelCommand(levelManager));
      keyboardController.PressedMappings.Add(Keys.Y, new NextLevelCommand(levelManager));
      keyboardController.PressedMappings.Add(Keys.H, new ToggleUpdatesCommand());
    }

    return keyboardController;
  }

  public static MouseController CreateMouseController(ILevelManager levelManager) {
    var mouseController = new MouseController();

    // Debug button binds:
    if (Flags.DebugButtonBinds) {
      mouseController.PressedMappings.Add(MouseButtons.Right, new PreviousLevelCommand(levelManager));
      mouseController.PressedMappings.Add(MouseButtons.Left, new NextLevelCommand(levelManager));
    }

    return mouseController;
  }

  public static GamePadController CreateGamePadController(Game1 game, Player player, ILevelManager levelManager) {
    // The gamepad bindings don't match the readme. this is intentional, because
    // the readme is in Xbox controller layout, but testing with a
    // nintendo pro controller seems to suggest it is pro controller layout.
    var gamePadController = new GamePadController(
      pressedMappings: new Dictionary<GPGamePadButtons, IGPCommand> {
        { GPGamePadButtons.BigButton, new QuitCommand(game) },
        { GPGamePadButtons.Back, new ReturnToMenuAndResetCommand(game) },
        { GPGamePadButtons.Start, new PauseCommand(game) },
        { GPGamePadButtons.RightTrigger, new OpenItemScreenCommand(game) },
        { GPGamePadButtons.A, new PlayerUseItemCommand(player, UseType.Pressed) },
        { GPGamePadButtons.B, new PlayerUseKeyCommand(player, UseType.Pressed) },
        { GPGamePadButtons.X, new PlayerInteractCommand(player) },
        { GPGamePadButtons.LeftTrigger, new PlayerSwapWeaponCommand(player) },
        { GPGamePadButtons.DPadDown, new PlayerDropItemCommand(player) },
        { GPGamePadButtons.Y, new PlayerReloadWeaponCommand(player) },
        { GPGamePadButtons.RightThumbstickLeftStrict, new PlayerDieCommand(player) },
        { GPGamePadButtons.RightShoulder, new OpenSavePromptCommand(game) },
        { GPGamePadButtons.LeftShoulder, new OpenLoadPromptCommand(game) },
        { GPGamePadButtons.RightThumbstickUpStrict, new ToggleMusicCommand() },
      },
      downMappings: new Dictionary<GPGamePadButtons, IGPCommand> {
        { GPGamePadButtons.A, new PlayerUseItemCommand(player, UseType.Held) },
        { GPGamePadButtons.LeftThumbstickUp, new PlayerMoveUpCommand(player) },
        { GPGamePadButtons.LeftThumbstickDown, new PlayerMoveDownCommand(player) },
        { GPGamePadButtons.LeftThumbstickLeft, new PlayerMoveLeftCommand(player) },
        { GPGamePadButtons.LeftThumbstickRight, new PlayerMoveRightCommand(player) },
      },
      releasedMappings: new Dictionary<GPGamePadButtons, IGPCommand> {
        { GPGamePadButtons.A, new PlayerUseItemCommand(player, UseType.Released) },
      }
    );

    // Debug button binds:
    if (Flags.DebugButtonBinds) {
      gamePadController.PressedMappings.Add(GPGamePadButtons.DPadLeft, new PreviousLevelCommand(levelManager));
      gamePadController.PressedMappings.Add(GPGamePadButtons.DPadRight, new NextLevelCommand(levelManager));
      gamePadController.PressedMappings.Add(GPGamePadButtons.DPadUp, new ToggleUpdatesCommand());
    }

    return gamePadController;
  }
}
