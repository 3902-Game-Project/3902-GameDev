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

    // The bindings don't match the readme. this is intentional, because
    // the readme is in Xbox controller layout, but testing with a
    // nintendo pro controller seems to suggest it is pro controller layout.

    if (gamePadState.Buttons.A == ButtonState.Pressed && prevGamePadState.Buttons.A == ButtonState.Released) {
      startGameCommand.Execute();
    } else if (gamePadState.Buttons.B == ButtonState.Pressed && prevGamePadState.Buttons.B == ButtonState.Released) {
      quitCommand.Execute();
    }
  }
}
