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
  private static readonly string START_TEXT = "Press Enter/GamePadB to start!";
  private static readonly string QUIT_TEXT = "Press Q/GamePadY to quit";
  private IController keyboardController;
  private IController gamePadController;

  public void Initialize() {
    keyboardController = new KeyboardController(
      pressedMappings: new Dictionary<Keys, IGPCommand> {
        { Keys.Q, new QuitCommand(game) },
        { Keys.Enter, new StartGameCommand(game) },
        { Keys.S, new ToggleSlowModeCommand(game) },
      }
    );

    // The gamepad bindings don't match the readme. this is intentional, because
    // the readme is in Xbox controller layout, but testing with a
    // nintendo pro controller seems to suggest it is pro controller layout.
    gamePadController = new GamePadController(
      pressedMappings: new Dictionary<Buttons, IGPCommand> {
        { Buttons.X, new QuitCommand(game) },
        { Buttons.A, new StartGameCommand(game) },
        { Buttons.Y, new ToggleSlowModeCommand(game) },
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
    spriteBatch.DrawString(
      spriteFont: MiscAssetStore.Instance.MainFont,
      text: START_TEXT,
      position:
        new Vector2(
          game.DefaultViewport.Width,
          game.DefaultViewport.Height
        ) * 0.5f + new Vector2(0.0f, -10.0f),
      color: Color.White,
      origin: MiscAssetStore.Instance.MainFont.MeasureString(START_TEXT) * 0.5f,
      rotation: 0.0f,
      scale: 1.0f,
      effects: SpriteEffects.None,
      layerDepth: 0.0f
    );
    spriteBatch.DrawString(
      spriteFont: MiscAssetStore.Instance.MainFont,
      text: QUIT_TEXT,
      position:
        new Vector2(
          game.DefaultViewport.Width,
          game.DefaultViewport.Height
        ) * 0.5f + new Vector2(0.0f, 10.0f),
      color: Color.White,
      origin: MiscAssetStore.Instance.MainFont.MeasureString(QUIT_TEXT) * 0.5f,
      rotation: 0.0f,
      scale: 1.0f,
      effects: SpriteEffects.None,
      layerDepth: 0.0f
    );
    spriteBatch.End();
  }

  public void OnStateEnter(bool prevStateIsCurrentState) { }

  public void OnStateLeave(bool nextStateIsCurrentState) { }

  public void OnStateStartFadeIn(bool prevStateIsCurrentState) { }

  public void OnStateEndFadeOut(bool nextStateIsCurrentState) { }
}
