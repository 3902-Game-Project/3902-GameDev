using System.Collections.Generic;
using GameProject.Commands;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

public class GameKeyboardController(Game1 game) : IController {
  private Dictionary<Keys, ICommand> keyMappings = new Dictionary<Keys, ICommand> {
    {Keys.Q, new QuitCommand(game)},
    {Keys.R, new ReturnToMenuAndResetCommand(game)},
    {Keys.D1, new FixedSpriteCommand(game)},
    {Keys.D2, new AnimatedSpriteCommand(game)},
    {Keys.D3, new UpAndDownCommand(game)},
    {Keys.D4, new LeftAndRightAnimatedCommand(game)},
    {Keys.O, new EnemySnakeCommand(game)}
  };

  public void Update(GameTime gameTime) {
    KeyboardState keyboardState = Keyboard.GetState();

    foreach (Keys key in keyboardState.GetPressedKeys()) {
      if (keyMappings.TryGetValue(key, out ICommand command)) {
        command.Execute();
      }
    }
  }
}
