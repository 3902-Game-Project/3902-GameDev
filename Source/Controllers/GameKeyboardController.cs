using System.Collections.Generic;
using GameProject.AbstractClasses;
using GameProject.Commands;
using GameProject.Enums;
using GameProject.Interfaces;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

public class GameKeyboardController(Game1 game) : AKeyboardController {
  protected override Dictionary<Keys, ICommand> PressedMappings { get; } = new() {
    {Keys.Q, new QuitCommand(game)},
    {Keys.R, new ReturnToMenuAndResetCommand(game)},
    {Keys.Z, new PlayerUseItemCommand(game.StateGame.Player)},
    {Keys.N, new PlayerUseItemCommand(game.StateGame.Player)},
    {Keys.U, new PreviousItemCommand(game)},
    {Keys.I, new NextItemCommand(game)},
    {Keys.J, new UseCurrentItemCommand(game, UseType.Pressed)},
    {Keys.E, new PlayerDieCommand(game.StateGame.Player)},

    {Keys.D1, new SwitchItemCommand(game, ItemCategory.Sidearm)},
    {Keys.D2, new SwitchItemCommand(game, ItemCategory.Primary)},
    {Keys.D3, new SwitchItemCommand(game, ItemCategory.Consumable)},
    {Keys.D4, new SwitchItemCommand(game, ItemCategory.Melee)},
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
    {Keys.J, new UseCurrentItemCommand(game, UseType.Held)},
  };

  protected override Dictionary<Keys, ICommand> ReleasedMappings { get; } = new() { };
}
