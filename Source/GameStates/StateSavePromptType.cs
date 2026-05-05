using GameProject.ButtonDiffTrackers;
using GameProject.Controllers;
using GameProject.Factories.Controller;
using GameProject.GlobalInterfaces;
using GameProject.Globals;
using GameProject.HelperFuncs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameProject.GameStates;

internal class StateSavePromptType(Game1 game) : IGameState {
  private IController<Keys> keyboardController;
  private IController<GPGamePadButtons> gamePadController;
  private double successTimer = 0.0;

  public bool IsShowingSuccess { get; set; } = false;

  public void Initialize() {
    keyboardController = SavePromptControllerFactory.CreateKeyboardController(game, this);
    gamePadController = SavePromptControllerFactory.CreateGamePadController(game, this);
  }

  public void LoadContent(ContentManager content) { }

  public void Update(double deltaTime) {
    if (IsShowingSuccess) {
      successTimer -= deltaTime;
      if (successTimer <= 0) game.ChangeStateWithoutFading(game.StateGame);
    } else {
      keyboardController.Update();
      gamePadController.Update();
    }
  }

  public void LowLevelDraw(LowLevelDrawParams drawData) {
    drawData.ClearWindowCallback(new(25, 28, 33)); // Dark gray background

    drawData.SpriteBatch.Begin();

    string text = IsShowingSuccess
        ? "The game is successfully saved"
        : "Would you like to save the game?\nPress A/GamePadB to confirm, D/GamePadA to cancel.";

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

  public void OnStateEnter(bool prevStateIsCurrentState) {
    IsShowingSuccess = false;
    successTimer = 1.5; // Displays the success text for 1.5 seconds
  }
  public void OnStateLeave(bool nextStateIsCurrentState) { }
  public void OnStateStartFadeIn(bool prevStateIsCurrentState) { }
  public void OnStateEndFadeOut(bool nextStateIsCurrentState) { }
}
