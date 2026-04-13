using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;
using GameProject.Interfaces;

namespace GameProject.Controllers;

internal class GameMouseController(Game1 game) : AMouseController {
  protected override Dictionary<MouseButtons, ICommand> PressedMappings { get; } = new() {
    { MouseButtons.Right, new PreviousLevelCommand(game.StateGame.LevelManager) },
    { MouseButtons.Left, new NextLevelCommand(game.StateGame.LevelManager) },
  };

  protected override Dictionary<MouseButtons, ICommand> DownMappings { get; } = [];

  protected override Dictionary<MouseButtons, ICommand> ReleasedMappings { get; } = [];
}
