using System.Collections.Generic;
using GameProject.Commands;
using GameProject.Interfaces;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

internal class GameGamePadController(Game1 game) : AGamePadController {
  // The bindings don't match the readme. this is intentional, because
  // the readme is in Xbox controller layout, but testing with a
  // nintendo pro controller seems to suggest it is pro controller layout.

  protected override Dictionary<Buttons, ICommand> PressedMappings { get; } = new() {
    {Buttons.X, new QuitCommand(game)},
    {Buttons.B, new ReturnToMenuAndResetCommand(game)},
    {Buttons.A, new PlayerUseItemCommand(game.StateGame.Player, UseType.Pressed)},
    {Buttons.Y, new PlayerDieCommand(game.StateGame.Player)},
    {Buttons.LeftShoulder, new PreviousLevelCommand(game.StateGame.LevelManager)},
    {Buttons.RightShoulder, new NextLevelCommand(game.StateGame.LevelManager)},
  };

  protected override Dictionary<Buttons, ICommand> DownMappings { get; } = new() {
    {Buttons.A, new PlayerUseItemCommand(game.StateGame.Player, UseType.Held)},
    {Buttons.DPadUp, new PlayerMoveUpCommand(game.StateGame.Player)},
    {Buttons.DPadDown, new PlayerMoveDownCommand(game.StateGame.Player)},
    {Buttons.DPadLeft, new PlayerMoveLeftCommand(game.StateGame.Player)},
    {Buttons.DPadRight, new PlayerMoveRightCommand(game.StateGame.Player)},
  };

  protected override Dictionary<Buttons, ICommand> ReleasedMappings { get; } = new() {
    {Buttons.A, new PlayerUseItemCommand(game.StateGame.Player, UseType.Released)},
  };
}
