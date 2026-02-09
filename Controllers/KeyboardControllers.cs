using System.Collections.Generic;
using GameProject.Commands;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

public class KeyboardController : IController {
  private Dictionary<Keys, ICommand> keyMappings;

  public KeyboardController(Game1 game) {
    keyMappings = new Dictionary<Keys, ICommand> {
      {Keys.D0, new QuitCommand(game)},
      {Keys.D1, new FixedSpriteCommand(game)},
      {Keys.D2, new AnimatedSpriteCommand(game)},
      {Keys.D3, new UpandDownCommand(game)},
      {Keys.D4, new LeftandRightAnimatedCommand(game)}
    };
  }

  public void Update(GameTime gameTime) {
    KeyboardState keyboardState = Keyboard.GetState();

    foreach (Keys key in keyMappings.Keys) {
      if (keyboardState.IsKeyDown(key)) {
        keyMappings[key].Execute();
      }
    }
  }
}
