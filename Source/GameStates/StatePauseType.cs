using GameProject.Controllers;
using GameProject.Globals;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameStates;

public class StatePauseType(Game1 game) : IGameState {
  private static readonly string TITLE_TEXT = "Paused";
  private static readonly string RETURN_TEXT = "Press P/GamePadB to return to game, Q/GamePadY to quit.";
  private IController keyboardController;
  private IController gamePadController;

  public void Initialize() {
    keyboardController = new PauseKeyboardController(game);
    gamePadController = new PauseGamePadController(game);
  }

  public void LoadContent(ContentManager content) { }

  public void Update(GameTime gameTime) {
    keyboardController.Update(gameTime);
    gamePadController.Update(gameTime);
  }

  public void LowLevelDraw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch) {
    graphicsDevice.Clear(Color.CornflowerBlue);

    spriteBatch.Begin();
    spriteBatch.DrawString(
      spriteFont: MiscAssetStore.MainFont,
      text: TITLE_TEXT,
      position:
        new Vector2(
          game.Window.ClientBounds.Width,
          game.Window.ClientBounds.Height
        ) * 0.5f + new Vector2(0.0f, -10.0f),
      color: Color.White,
      origin: MiscAssetStore.MainFont.MeasureString(TITLE_TEXT) * 0.5f,
      rotation: 0.0f,
      scale: 1.0f,
      effects: SpriteEffects.None,
      layerDepth: 0.0f
    );
    spriteBatch.DrawString(
      spriteFont: MiscAssetStore.MainFont,
      text: RETURN_TEXT,
      position:
        new Vector2(
          game.Window.ClientBounds.Width,
          game.Window.ClientBounds.Height
        ) * 0.5f + new Vector2(0.0f, 10.0f),
      color: Color.White,
      origin: MiscAssetStore.MainFont.MeasureString(RETURN_TEXT) * 0.5f,
      rotation: 0.0f,
      scale: 1.0f,
      effects: SpriteEffects.None,
      layerDepth: 0.0f
    );
    spriteBatch.End();
  }

  public void OnStateEnter() { }

  public void OnStateLeave() { }

  public void OnStateStartFadeIn() { }

  public void OnStateEndFadeOut() { }
}
