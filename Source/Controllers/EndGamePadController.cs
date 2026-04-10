using System.Collections.Generic;
using GameProject.Commands;
using GameProject.Interfaces;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

internal class EndGamePadController(Game1 game) : AGamePadController {
  // The bindings don't match the readme. this is intentional, because
  // the readme is in Xbox controller layout, but testing with a
  // nintendo pro controller seems to suggest it is pro controller layout.
  protected override Dictionary<Buttons, ICommand> PressedMappings { get; } = new() {
    {Buttons.B, new ReturnToMenuAndResetCommand(game)},
    {Buttons.X, new QuitCommand(game)},
  };

  protected override Dictionary<Buttons, ICommand> DownMappings { get; } = [];

  protected override Dictionary<Buttons, ICommand> ReleasedMappings { get; } = [];
}
