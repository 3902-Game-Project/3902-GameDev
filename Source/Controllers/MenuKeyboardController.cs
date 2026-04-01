using System.Collections.Generic;
using GameProject.AbstractClasses;
using GameProject.Commands;
using GameProject.Interfaces;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

public class MenuKeyboardController(Game1 game) : AKeyboardController {
  protected override Dictionary<Keys, ICommand> PressedMappings { get; } = new() {
    {Keys.Q, new QuitCommand(game)},
    {Keys.Enter, new StartGameCommand(game)},
  };

  protected override Dictionary<Keys, ICommand> DownMappings { get; } = [];

  protected override Dictionary<Keys, ICommand> ReleasedMappings { get; } = [];
}
