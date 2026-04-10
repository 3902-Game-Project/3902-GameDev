using System.Collections.Generic;
using GameProject.Commands;
using GameProject.Interfaces;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

public class ItemKeyboardController(Game1 game) : AKeyboardController {
  protected override Dictionary<Keys, ICommand> PressedMappings { get; } = new() {
    {Keys.R, new ReturnToGameCommand(game)},
    {Keys.Q, new QuitCommand(game)},
  };

  protected override Dictionary<Keys, ICommand> DownMappings { get; } = [];

  protected override Dictionary<Keys, ICommand> ReleasedMappings { get; } = [];
}
