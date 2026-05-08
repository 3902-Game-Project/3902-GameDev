using GameProject.ButtonDiffTrackers;
using GameProject.Controllers;
using GameProject.Controllers.Factories;
using GameProject.GlobalInterfaces;
using GameProject.Globals;
using GameProject.HelperFuncs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject.GameStates;

internal class StateMenuType(Game1 game) : IGameState {
  private IController<Keys, KeyboardState> keyboardController;
  private IController<GPGamePadButtons, GamePadState> gamePadController;

  internal void Initialize() {
    keyboardController = MenuControllerFactory.CreateKeyboardController(game);
    gamePadController = MenuControllerFactory.CreateGamePadController(game);
  }

  internal void LoadContent(ContentManager content) { }

  internal void Update(double deltaTime, bool isActive) {
    keyboardController.Update(Keyboard.GetState());
    gamePadController.Update(GamePad.GetState(Constants.GAMEPAD_PLAYER_INDEX));
  }

  internal void LowLevelDraw(LowLevelDrawParams drawData) {
    drawData.SpriteBatch.Begin();

    drawData.SpriteBatch.Draw(
      texture: TextureStore.Instance.TitleScreen,
      position: new(-50.0f, 0.0f),
      sourceRectangle: new Rectangle(0, 0, 1028, 704), // FIX
      color: Color.White,
      rotation: 0f,
      origin: Vector2.Zero,
      scale: 1f,
      effects: SpriteEffects.None,
      layerDepth: 0f
    );

    TextFuncs.DrawCenteredString(
      spriteBatch: drawData.SpriteBatch,
      position:
        new Vector2(
          Constants.WINDOW_WIDTH,
          Constants.WINDOW_HEIGHT
        ) * 0.5f,
      text:
        "Press Enter/GamePadB to start!\n" +
        "Press Q/GamePadY to quit.",
      color: Color.Black
    );

    TextFuncs.DrawCenteredString(
      spriteBatch: drawData.SpriteBatch,
      position:
        new Vector2(
          Constants.WINDOW_WIDTH,
          Constants.WINDOW_HEIGHT
        ) * 0.5f +
        new Vector2(0.0f, 60.0f),
      text:
        $"Slow Reaction Time Mode (0.5x Speed): {(Flags.SlowMode ? "Enabled" : "Disabled")}\n" +
        "(Press S/GamePadX to toggle)",
      color: Color.Black
    );

    drawData.SpriteBatch.End();
  }

  internal void OnStateEnter(bool prevStateIsCurrentState) { }

  internal void OnStateLeave(bool nextStateIsCurrentState) { }

  internal void OnStateStartFadeIn(bool prevStateIsCurrentState) { }

  internal void OnStateEndFadeOut(bool nextStateIsCurrentState) { }
}
