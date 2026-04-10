using GameProject.Controllers;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameStates;

public class StateMenuType(Game1 game) : IGameState {
  private static readonly string START_TEXT = "Press Enter/GamePadB to start!";
  private IController keyboardController;
  private IController gamePadController;

  public void Initialize() {
    keyboardController = new MenuKeyboardController(game);
    gamePadController = new MenuGamePadController(game);
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
      spriteFont: game.Assets.MainFont,
      text: START_TEXT,
      position:
        new Vector2(
          game.Window.ClientBounds.Width,
          game.Window.ClientBounds.Height
        ) * 0.5f,
      color: Color.White,
      origin: game.Assets.MainFont.MeasureString(START_TEXT) * 0.5f,
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
