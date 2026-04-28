using System.Collections.Generic;
using GameProject.ButtonDiffTrackers;
using GameProject.Commands;
using GameProject.Controllers;
using GameProject.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject.GameStates;

internal class StateWinType(Game1 game) : IGameState {
  private IController<Keys> keyboardController;
  private IController<GPGamePadButtons> gamePadController;

  public void Initialize() {
    keyboardController = new KeyboardController(
      pressedMappings: new Dictionary<Keys, IGPCommand> {
        { Keys.Q, new QuitCommand(game) },
        { Keys.Back, new ReturnToMenuAndResetCommand(game) },
      }
    );

    // The gamepad bindings don't match the readme. this is intentional, because
    // the readme is in Xbox controller layout, but testing with a
    // nintendo pro controller seems to suggest it is pro controller layout.
    gamePadController = new GamePadController(
      pressedMappings: new Dictionary<GPGamePadButtons, IGPCommand> {
        { GPGamePadButtons.X, new QuitCommand(game) },
        { GPGamePadButtons.B, new ReturnToMenuAndResetCommand(game) },
      }
    );
  }

  public void LoadContent(ContentManager content) { }

  public void Update(double deltaTime) {
    keyboardController.Update();
    gamePadController.Update();
  }

  public void LowLevelDraw(GraphicsDevice graphicsDevice, RenderTargetTracker renderTargetTracker, SpriteBatch spriteBatch) {
    graphicsDevice.Clear(Color.CornflowerBlue);

    spriteBatch.Begin();

    TextFuncs.DrawCenteredString(
      spriteBatch: spriteBatch,
      position: new Vector2(
          game.DefaultViewport.Width,
          game.DefaultViewport.Height
        ) * 0.5f,
      text:
        "You've won!\n" +
        "Press Backspace/GamePadA for main menu, Q/GamePadY to quit."
    );

    spriteBatch.End();
  }

  public void OnStateEnter(bool prevStateIsCurrentState) { }

  public void OnStateLeave(bool nextStateIsCurrentState) { }

  public void OnStateStartFadeIn(bool prevStateIsCurrentState) { }

  public void OnStateEndFadeOut(bool nextStateIsCurrentState) { }
}
