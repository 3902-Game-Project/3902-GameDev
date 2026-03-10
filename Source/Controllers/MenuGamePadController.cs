using GameProject.Commands;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

internal class MenuGamePadController(Game1 game) : IController {
  private GamePadState prevGamePadState = new();
  private GamePadState gamePadState = new();
  private ICommand quitCommand = new QuitCommand(game);
  private ICommand startGameCommand = new StartGameCommand(game);

  public void Update(GameTime gameTime) {
    prevGamePadState = gamePadState;
    gamePadState = GamePad.GetState(PlayerIndex.One);

    if (gamePadState.Buttons.A == ButtonState.Pressed && prevGamePadState.Buttons.A == ButtonState.Released) {
      startGameCommand.Execute();
    } else if (gamePadState.Buttons.A == ButtonState.Pressed && prevGamePadState.Buttons.A == ButtonState.Released) {
      quitCommand.Execute();
    }
  }
}
