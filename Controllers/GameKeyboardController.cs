using System.Collections.Generic;
using GameProject.Commands;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

public class GameKeyboardController(Game1 game) : IController {
  private KeyboardState previousState;
  private KeyboardState currentState;

  private Dictionary<Keys, ICommand> keyMappings = new Dictionary<Keys, ICommand> {
    {Keys.Q, new QuitCommand(game)},
    {Keys.R, new ReturnToMenuAndResetCommand(game)},
    {Keys.D1, new FixedSpriteCommand(game)},
    {Keys.D2, new AnimatedSpriteCommand(game)},
    {Keys.D3, new UpAndDownCommand(game)},
    {Keys.D4, new LeftAndRightAnimatedCommand(game)},
    {Keys.O, new EnemySnakeCommand(game)},
    {Keys.I, new EnemyBatCommand(game)},
    {Keys.U, new EnemyShotgunnerCommand(game)},
    {Keys.Y, new NextBlockCommand(game)},
    {Keys.T, new PreviousBlockCommand(game)},
    {Keys.Z, new PlayerUseItemCommand(game)},
    {Keys.N, new PlayerUseItemCommand(game)},

    {Keys.W, new PlayerMoveUpCommand(game)},
    {Keys.S, new PlayerMoveDownCommand(game)},
    {Keys.A, new PlayerMoveLeftCommand(game)},
    {Keys.D, new PlayerMoveRightCommand(game)},
  };

  public void Update(GameTime gameTime) {
    previousState = currentState;
    currentState = Keyboard.GetState();

    foreach (Keys key in currentState.GetPressedKeys()) {
      if (((key == Keys.Y) || (key == Keys.T)) && previousState.IsKeyDown(key)) continue; // added for sprint2 -Aaron

      if (keyMappings.TryGetValue(key, out ICommand command)) command.Execute();
    }
  }
}
