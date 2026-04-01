using GameProject.Commands;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

internal class GameMouseController(Game1 game) : IController {
  private MouseState prevMouseState = new();
  private MouseState mouseState = new();

  private readonly ICommand prevLevelCommand = new PreviousLevelCommand(game.StateGame.LevelManager);
  private readonly ICommand nextLevelCommand = new NextLevelCommand(game.StateGame.LevelManager);

  public void Update(GameTime gameTime) {
    prevMouseState = mouseState;
    mouseState = Mouse.GetState();

    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released) {
      nextLevelCommand.Execute();
    }

    if (mouseState.RightButton == ButtonState.Pressed && prevMouseState.RightButton == ButtonState.Released) {
      prevLevelCommand.Execute();
    }
  }
}
