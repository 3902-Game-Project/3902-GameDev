using System.Collections.Generic;
using GameProject.AbstractClasses;
using GameProject.Commands;
using GameProject.Interfaces;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

public class GameKeyboardController(Game1 game) : AKeyboardController {
  protected override Dictionary<Keys, ICommand> PressedMappings { get; } = new() {
    {Keys.R, new ReturnToMenuAndResetCommand(game)},
    {Keys.Q, new QuitCommand(game)},
    {Keys.J, new PlayerUseItemCommand(game.StateGame.Player, UseType.Pressed)},
    {Keys.E, new PlayerDieCommand(game.StateGame.Player)},

    {Keys.T, new PreviousLevelCommand(game.StateGame.LevelManager)},
    {Keys.Y, new NextLevelCommand(game.StateGame.LevelManager)},

    { Keys.F, new PlayerInteractCommand(game.StateGame.Player) },
    { Keys.Space, new SwapWeaponCommand(game.StateGame.Player) },
    { Keys.Tab, new ToggleMusicCommand() }
  };

  protected override Dictionary<Keys, ICommand> DownMappings { get; } = new() {
    {Keys.W, new PlayerMoveUpCommand(game.StateGame.Player)},
    {Keys.S, new PlayerMoveDownCommand(game.StateGame.Player)},
    {Keys.A, new PlayerMoveLeftCommand(game.StateGame.Player)},
    {Keys.D, new PlayerMoveRightCommand(game.StateGame.Player)},
    {Keys.Up, new PlayerMoveUpCommand(game.StateGame.Player)},
    {Keys.Down, new PlayerMoveDownCommand(game.StateGame.Player)},
    {Keys.Left, new PlayerMoveLeftCommand(game.StateGame.Player)},
    {Keys.Right, new PlayerMoveRightCommand(game.StateGame.Player)},
    {Keys.J, new PlayerUseItemCommand(game.StateGame.Player, UseType.Held)}
  };

  protected override Dictionary<Keys, ICommand> ReleasedMappings { get; } = new() {
    {Keys.J, new PlayerUseItemCommand(game.StateGame.Player, UseType.Released)},
  };
}
