using System.Collections.Generic;
using GameProject.Commands;
using GameProject.Interfaces;
using GameProject.GameStates; // Needed for StateItemScreenType
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

public class ItemScreenKeyboardController(Game1 game, StateItemScreenType screen) : AKeyboardController {

  protected override Dictionary<Keys, ICommand> PressedMappings { get; } = new() {
    { Keys.I, new ReturnToGameCommand(game) },
    { Keys.Q, new QuitCommand(game) },
    
    // Add the new navigation bindings
    { Keys.A, new MenuMoveLeftCommand(screen) },
    { Keys.Left, new MenuMoveLeftCommand(screen) },

    { Keys.D, new MenuMoveRightCommand(screen) },
    { Keys.Right, new MenuMoveRightCommand(screen) },

    { Keys.Enter, new MenuEquipCommand(screen) },
    { Keys.Space, new MenuEquipCommand(screen) }
  };

  protected override Dictionary<Keys, ICommand> DownMappings { get; } = [];

  protected override Dictionary<Keys, ICommand> ReleasedMappings { get; } = [];
}
