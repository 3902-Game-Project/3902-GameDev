using GameProject.Commands;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

internal class ItemScreenGamePadController(Game1 game) : IController {
  private GamePadState prevGamePadState = new();
  private GamePadState gamePadState = new();
  private readonly ICommand quitCommand = new QuitCommand(game);
  private readonly ICommand returnToGameCommand = new ReturnToGameCommand(game);

  public void Update(GameTime gameTime) {
    prevGamePadState = gamePadState;
    gamePadState = GamePad.GetState(PlayerIndex.One);

    // The bindings don't match the readme. this is intentional, because
    // the readme is in Xbox controller layout, but testing with a
    // nintendo pro controller seems to suggest it is pro controller layout.

    if (gamePadState.Buttons.A == ButtonState.Pressed && prevGamePadState.Buttons.A == ButtonState.Released) {
      returnToGameCommand.Execute();
    }

    if (gamePadState.Buttons.X == ButtonState.Pressed && prevGamePadState.Buttons.X == ButtonState.Released) {
      quitCommand.Execute();
    }
  }
}
