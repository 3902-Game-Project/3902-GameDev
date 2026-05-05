using GameProject.ButtonDiffTrackers;
using GameProject.Controllers;
using GameProject.Factories.Controller;
using GameProject.Globals;
using GameProject.HelperFuncs;
using GameProject.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject.GameStates;

internal class StateMenuType(Game1 game) : IGameState {
  private IController<Keys> keyboardController;
  private IController<GPGamePadButtons> gamePadController;

  public void Initialize() {
    keyboardController = MenuControllerFactory.CreateKeyboardController(game);
    gamePadController = MenuControllerFactory.CreateGamePadController(game);
  }

  public void LoadContent(ContentManager content) { }

  public void Update(double deltaTime) {
    keyboardController.Update();
    gamePadController.Update();
  }

  public void LowLevelDraw(GraphicsDevice graphicsDevice, RenderTargetTracker renderTargetTracker, SpriteBatch spriteBatch) {
    graphicsDevice.Clear(Color.CornflowerBlue);

    spriteBatch.Begin();

    spriteBatch.Draw(
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
      spriteBatch: spriteBatch,
      position: new Vector2(
          game.DefaultViewport.Width,
          game.DefaultViewport.Height
        ) * 0.5f,
      text:
        "Press Enter/GamePadB to start!\n" +
        "Press Q/GamePadY to quit.",
      color: Color.Black
    );

    TextFuncs.DrawCenteredString(
      spriteBatch: spriteBatch,
      position: new Vector2(
          game.DefaultViewport.Width,
          game.DefaultViewport.Height
        ) * 0.5f + new Vector2(0.0f, 60.0f),
      text:
        $"Slow Reaction Time Mode (0.5x Speed): {(Flags.SlowMode ? "Enabled" : "Disabled")}\n" +
        "(Press S/GamePadX to toggle)",
      color: Color.Black
    );

    spriteBatch.End();
  }

  public void OnStateEnter(bool prevStateIsCurrentState) { }

  public void OnStateLeave(bool nextStateIsCurrentState) { }

  public void OnStateStartFadeIn(bool prevStateIsCurrentState) { }

  public void OnStateEndFadeOut(bool nextStateIsCurrentState) { }
}
