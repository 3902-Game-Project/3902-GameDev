using GameProject.Controllers;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameStates;

public class StateLossType(Game1 game) : IGameState {
  private static readonly string LOSS_TEXT = "Game Over";
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
      text: LOSS_TEXT,
      position:
        new Vector2(
          game.Window.ClientBounds.Width,
          game.Window.ClientBounds.Height
        ) * 0.5f,
      color: Color.White,
      origin: game.Assets.MainFont.MeasureString(LOSS_TEXT) * 0.5f,
      rotation: 0.0f,
      scale: 1.0f,
      effects: SpriteEffects.None,
      layerDepth: 0.0f
    );
    game.SpriteBatch.End();
  }
}
