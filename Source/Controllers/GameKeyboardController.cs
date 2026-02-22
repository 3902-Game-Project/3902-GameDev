using System.Collections.Generic;
using GameProject.Commands;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

public class GameKeyboardController(Game1 game) : IController {
  private KeyboardState previousState;
  private KeyboardState currentState;

  private readonly Dictionary<Keys, ICommand> keyMappings = new() {
    {Keys.Q, new QuitCommand(game)},
    {Keys.R, new ReturnToMenuAndResetCommand(game)},
    {Keys.Y, new NextBlockCommand(game)},
    {Keys.T, new PreviousBlockCommand(game)},
    {Keys.Z, new PlayerUseItemCommand(game.StateGame.Player)},
    {Keys.N, new PlayerUseItemCommand(game.StateGame.Player)},
    {Keys.U, new PreviousItemCommand(game)},
    {Keys.I, new NextItemCommand(game)},
    {Keys.J, new UseCurrentItemCommand(game)},

    {Keys.W, new PlayerMoveUpCommand(game.StateGame.Player)},
    {Keys.S, new PlayerMoveDownCommand(game.StateGame.Player)},
    {Keys.A, new PlayerMoveLeftCommand(game.StateGame.Player)},
    {Keys.D, new PlayerMoveRightCommand(game.StateGame.Player)},
    {Keys.Up, new PlayerMoveUpCommand(game.StateGame.Player)},
    {Keys.Down, new PlayerMoveDownCommand(game.StateGame.Player)},
    {Keys.Left, new PlayerMoveLeftCommand(game.StateGame.Player)},
    {Keys.Right, new PlayerMoveRightCommand(game.StateGame.Player)},
  };

  public void Update(GameTime gameTime) {
    previousState = currentState;
    currentState = Keyboard.GetState();

    foreach (Keys key in currentState.GetPressedKeys()) {
      if (((key == Keys.Y) || (key == Keys.T) || (key == Keys.U) || (key == Keys.I)) && previousState.IsKeyDown(key)) {
        continue; // added for sprint2 -Aaron
      }

      if (keyMappings.TryGetValue(key, out ICommand command)) {
        command.Execute();
      }
    }
  }
}
