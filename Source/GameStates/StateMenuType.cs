using System.Collections.Generic;
using GameProject.Commands;
using GameProject.Controllers;
using GameProject.Globals;
using GameProject.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject.GameStates;

internal class StateMenuType(Game1 game) : IGameState {
  private IController<Keys> keyboardController;
  private IController<Buttons> gamePadController;

  public void Initialize() {
    keyboardController = new KeyboardController(
      pressedMappings: new Dictionary<Keys, IGPCommand> {
        { Keys.Q, new QuitCommand(game) },
        { Keys.Enter, new StartGameCommand(game) },
        { Keys.S, new ToggleSlowModeCommand() },
      }
    );

    // The gamepad bindings don't match the readme. this is intentional, because
    // the readme is in Xbox controller layout, but testing with a
    // nintendo pro controller seems to suggest it is pro controller layout.
    gamePadController = new GamePadController(
      pressedMappings: new Dictionary<Buttons, IGPCommand> {
        { Buttons.X, new QuitCommand(game) },
        { Buttons.A, new StartGameCommand(game) },
        { Buttons.Y, new ToggleSlowModeCommand() },
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
        "Press Enter/GamePadB to start!\n" +
        "Press Q/GamePadY to quit."
    );

    TextFuncs.DrawCenteredString(
      spriteBatch: spriteBatch,
      position: new Vector2(
          game.DefaultViewport.Width,
          game.DefaultViewport.Height
        ) * 0.5f + new Vector2(0.0f, 60.0f),
      text:
        $"Slow Reaction Time Mode (0.5x Speed): {(Flags.SlowMode ? "Enabled" : "Disabled")}\n" +
        "(Press S/GamePadX to toggle)"
    );

    spriteBatch.End();
  }

  public void OnStateEnter(bool prevStateIsCurrentState) { }

  public void OnStateLeave(bool nextStateIsCurrentState) { }

  public void OnStateStartFadeIn(bool prevStateIsCurrentState) { }

  public void OnStateEndFadeOut(bool nextStateIsCurrentState) { }
}
