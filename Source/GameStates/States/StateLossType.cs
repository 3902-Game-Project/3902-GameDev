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

internal class StateLossType(Game1 game) : IGameState {
  private IController<Keys, KeyboardState> keyboardController;
  private IController<GPGamePadButtons, GamePadState> gamePadController;

  public void Initialize() {
    keyboardController = WinLossControllerFactory.CreateKeyboardController(game);
    gamePadController = WinLossControllerFactory.CreateGamePadController(game);
  }

  public void LoadContent(ContentManager content) { }

  public void Update(double deltaTime, bool isActive) {
    keyboardController.Update(Keyboard.GetState());
    gamePadController.Update(GamePad.GetState(Constants.GAMEPAD_PLAYER_INDEX));
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
        "Game Over\n" +
        "Press Backspace/GamePadA for main menu, Q/GamePadY to quit."
    );

    drawData.SpriteBatch.End();
  }

  public void OnStateEnter(bool prevStateIsCurrentState) { }

  public void OnStateLeave(bool nextStateIsCurrentState) { }

  public void OnStateStartFadeIn(bool prevStateIsCurrentState) { }

  public void OnStateEndFadeOut(bool nextStateIsCurrentState) { }
}
