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
  private IController<Keys, KeyboardState> keyboardController;
  private IController<GPGamePadButtons, GamePadState> gamePadController;

  internal void Initialize() {
    keyboardController = PauseControllerFactory.CreateKeyboardController(game);
    gamePadController = PauseControllerFactory.CreateGamePadController(game);
  }

  internal void LoadContent(ContentManager content) { }

  internal void Update(double deltaTime, bool isActive) {
    keyboardController.Update(Keyboard.GetState());
    gamePadController.Update(GamePad.GetState(Constants.GAMEPAD_PLAYER_INDEX));
  }

  internal void LowLevelDraw(LowLevelDrawParams drawData) {
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

  internal void OnStateEnter(bool prevStateIsCurrentState) { }

  internal void OnStateLeave(bool nextStateIsCurrentState) { }

  internal void OnStateStartFadeIn(bool prevStateIsCurrentState) { }

  internal void OnStateEndFadeOut(bool nextStateIsCurrentState) { }
}
