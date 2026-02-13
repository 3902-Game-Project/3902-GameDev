using GameProject.Controllers;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameStates;

public class StateMenuType(Game1 game) : IGameState {
  private static readonly string START_TEXT = "Press Enter to start!";
  private IController keyboardController;

  public void Initialize() {
    keyboardController = new MenuKeyboardController(game);
  }

  public void LoadContent() { }

  public void Update(GameTime gameTime) {
    keyboardController.Update(gameTime);
  }

  public void Draw(GameTime gameTime) {
    game.GraphicsDevice.Clear(Color.CornflowerBlue);

    game.SpriteBatch.Begin();
    game.SpriteBatch.DrawString(
      spriteFont: game.GlobalVars.Assets.MainFont,
      text: START_TEXT,
      position:
        new Vector2(
          game.Window.ClientBounds.Width,
          game.Window.ClientBounds.Height
        ) * 0.5f +
        new Vector2(0.0f, 150.0f),
      color: Color.White,
      origin: game.GlobalVars.Assets.MainFont.MeasureString(START_TEXT) * 0.5f,
      rotation: 0.0f,
      scale: 0.3f,
      effects: SpriteEffects.None,
      layerDepth: 0.0f
    );
    game.SpriteBatch.End();
  }
}
