using System.Collections.Generic;
using GameProject.Commands;
using GameProject.Controllers;
using GameProject.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject.GameStates;

internal class StateLossType(Game1 game) : IGameState {
  private IController keyboardController;
  private IController gamePadController;

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
      pressedMappings: new Dictionary<Buttons, IGPCommand> {
        { Buttons.X, new QuitCommand(game) },
        { Buttons.B, new ReturnToMenuAndResetCommand(game) },
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

    TextFuncs.DrawCenteredText(
      spriteBatch: spriteBatch,
      position: new Vector2(
          game.DefaultViewport.Width,
          game.DefaultViewport.Height
        ) * 0.5f,
      text:
        "Game Over\n" +
        "Press R/GamePadA for main menu, Q/GamePadY to quit."
    );

    spriteBatch.End();
  }

  public void OnStateEnter(bool prevStateIsCurrentState) { }

  public void OnStateLeave(bool nextStateIsCurrentState) { }

  public void OnStateStartFadeIn(bool prevStateIsCurrentState) { }

  public void OnStateEndFadeOut(bool nextStateIsCurrentState) { }
}
