using System.Collections.Generic;
using GameProject.Commands;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

public class MenuKeyboardController(Game1 game) : IController {
  private Dictionary<Keys, ICommand> keyMappings = new Dictionary<Keys, ICommand> {
    {Keys.Q, new QuitCommand(game)},
    {Keys.Enter, new StartGameCommand(game)}
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
