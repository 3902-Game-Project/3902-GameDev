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

internal class StateLoadPromptType(Game1 game) : IGameState {
  private IController<Keys, KeyboardState> keyboardController;
  private IController<GPGamePadButtons, GamePadState> gamePadController;
  private double successTimer = 0.0;

  internal bool IsShowingSuccess { get; set; } = false;

  internal void Initialize() {
    keyboardController = LoadPromptControllerFactory.CreateKeyboardController(game, this);
    gamePadController = LoadPromptControllerFactory.CreateGamePadController(game, this);
  }

  internal void LoadContent(ContentManager content) { }

  internal void Update(double deltaTime, bool isActive) {
    if (IsShowingSuccess) {
      successTimer -= deltaTime;
      if (successTimer <= 0) game.StateMachine.ChangeStateWithoutFading(GameState.StateGame);
    } else {
      keyboardController.Update(Keyboard.GetState());
      gamePadController.Update(GamePad.GetState(Constants.GAMEPAD_PLAYER_INDEX));
    }
  }

  internal void LowLevelDraw(LowLevelDrawParams drawData) {
    drawData.ClearWindowCallback(Constants.MAIN_BACKGROUND_COLOR);

    drawData.SpriteBatch.Begin();

    string text = IsShowingSuccess
        ? "Progress successfully loaded"
        : "Would you like to load your last saved progress?\nPress A/GamePadB to confirm, D?GamePadA to cancel.";

    TextFuncs.DrawCenteredString(
      spriteBatch: drawData.SpriteBatch,
      position:
        new Vector2(
          Constants.WINDOW_WIDTH,
          Constants.WINDOW_HEIGHT
        ) * 0.5f,
      text: text,
      color: Color.White
    );

    drawData.SpriteBatch.End();
  }

  internal void OnStateEnter(bool prevStateIsCurrentState) {
    IsShowingSuccess = false;
    successTimer = 1.5;
  }
  internal void OnStateLeave(bool nextStateIsCurrentState) { }
  internal void OnStateStartFadeIn(bool prevStateIsCurrentState) { }
  internal void OnStateEndFadeOut(bool nextStateIsCurrentState) { }
}
