using GameProject.ButtonDiffTrackers;
using GameProject.Controllers;
using GameProject.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject.GameStates;

internal class StateLoadPromptType(Game1 game) : IGameState {
  private IController<Keys> keyboardController;
  private IController<GPGamePadButtons> gamePadController;
  private double successTimer = 0.0;

  public bool IsShowingSuccess { get; set; } = false;

  public void Initialize() {
    keyboardController = LoadPromptControllerFactory.CreateKeyboardController(game, this);
    gamePadController = LoadPromptControllerFactory.CreateGamePadController(game, this);
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

  public void LowLevelDraw(GraphicsDevice graphicsDevice, RenderTargetTracker renderTargetTracker, SpriteBatch spriteBatch) {
    graphicsDevice.Clear(new(25, 28, 33));

    spriteBatch.Begin();

    string text = IsShowingSuccess
        ? "Progress successfully loaded"
        : "Would you like to load your last saved progress?\nPress A/GamePadB to confirm, D?GamePadA to cancel.";

    TextFuncs.DrawCenteredString(
      spriteBatch: spriteBatch,
      position: new Vector2(game.DefaultViewport.Width, game.DefaultViewport.Height) * 0.5f,
      text: text,
      color: Color.White
    );

    spriteBatch.End();
  }

  public void OnStateEnter(bool prevStateIsCurrentState) {
    IsShowingSuccess = false;
    successTimer = 1.5;
  }
  public void OnStateLeave(bool nextStateIsCurrentState) { }
  public void OnStateStartFadeIn(bool prevStateIsCurrentState) { }
  public void OnStateEndFadeOut(bool nextStateIsCurrentState) { }
}
