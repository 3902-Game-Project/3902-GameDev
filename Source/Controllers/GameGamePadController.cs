using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject.Controllers;

internal class GameGamePadController(Game1 game) : IController {
  private GamePadState prevGamePadState = new();
  private GamePadState gamePadState = new();

  public void Update(GameTime gameTime) {
    prevGamePadState = gamePadState;
    gamePadState = GamePad.GetState(PlayerIndex.One);
  }
}
