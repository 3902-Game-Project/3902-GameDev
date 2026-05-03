using System.Collections.Generic;
using GameProject;
using GameProject.Commands;
using GameProject.Controllers;
using GameProject.Globals;
using GameProject.Managers;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework.Input;

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
}
