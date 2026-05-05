using GameProject.ButtonDiffTrackers;
using GameProject.Controllers;
using GameProject.Controllers.Factories;
using GameProject.GlobalInterfaces;
using GameProject.Globals;
using GameProject.HelperFuncs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameProject.GameStates;

internal class StatePauseType(Game1 game) : IGameState {
  private IController<Keys> keyboardController;
  private IController<GPGamePadButtons> gamePadController;

  public void Initialize() {
    keyboardController = PauseControllerFactory.CreateKeyboardController(game);
    gamePadController = PauseControllerFactory.CreateGamePadController(game);
  }

  public void LoadContent(ContentManager content) { }

  public void Update(double deltaTime) {
    keyboardController.Update();
    gamePadController.Update();
  }

  public void LowLevelDraw(LowLevelDrawParams drawData) {
    drawData.ClearWindowCallback(Constants.MAIN_BACKGROUND_COLOR);

    drawData.SpriteBatch.Begin();

    TextFuncs.DrawCenteredString(
      spriteBatch: drawData.SpriteBatch,
      position:
        new Vector2(
          Constants.WINDOW_WIDTH,
          Constants.WINDOW_HEIGHT
        ) * 0.5f,
      text:
        "Paused\n" +
        "Press P/GamePadB to return to game, Q/GamePadY to quit."
    );

    drawData.SpriteBatch.End();
  }

  public void OnStateEnter(bool prevStateIsCurrentState) { }

  public void OnStateLeave(bool nextStateIsCurrentState) { }

  public void OnStateStartFadeIn(bool prevStateIsCurrentState) { }

  public void OnStateEndFadeOut(bool nextStateIsCurrentState) { }
}
