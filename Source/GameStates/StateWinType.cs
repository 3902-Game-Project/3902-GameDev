using GameProject.Controllers;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameStates;

public class StateWinType(Game1 game) : IGameState {
  private static readonly string TITLE_TEXT = "You've won!";
  private static readonly string RETURN_TEXT = "Press R/GamePadA for main menu, Q/GamePadY to quit.";
  private IController keyboardController;
  private IController gamePadController;

  public void Initialize() {
    keyboardController = new EndKeyboardController(game);
    gamePadController = new EndGamePadController(game);
  }

  public void LoadContent() { }

  public void Update(GameTime gameTime) {
    keyboardController.Update(gameTime);
    gamePadController.Update(gameTime);
  }

  public void Draw(GameTime gameTime) {
    game.GraphicsDevice.Clear(Color.CornflowerBlue);

    game.SpriteBatch.Begin();
    game.SpriteBatch.DrawString(
      spriteFont: game.Assets.MainFont,
      text: TITLE_TEXT,
      position:
        new Vector2(
          game.Window.ClientBounds.Width,
          game.Window.ClientBounds.Height
        ) * 0.5f + new Vector2(0.0f, -10.0f),
      color: Color.White,
      origin: game.Assets.MainFont.MeasureString(TITLE_TEXT) * 0.5f,
      rotation: 0.0f,
      scale: 1.0f,
      effects: SpriteEffects.None,
      layerDepth: 0.0f
    );
    game.SpriteBatch.DrawString(
      spriteFont: game.Assets.MainFont,
      text: RETURN_TEXT,
      position:
        new Vector2(
          game.Window.ClientBounds.Width,
          game.Window.ClientBounds.Height
        ) * 0.5f + new Vector2(0.0f, 10.0f),
      color: Color.White,
      origin: game.Assets.MainFont.MeasureString(RETURN_TEXT) * 0.5f,
      rotation: 0.0f,
      scale: 1.0f,
      effects: SpriteEffects.None,
      layerDepth: 0.0f
    );
    game.SpriteBatch.End();
  }

  public void OnStateEnter() { }

  public void OnStateLeave() { }
}
