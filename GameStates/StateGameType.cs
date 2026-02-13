using GameProject.Controllers;
using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;

namespace GameProject.GameStates;

public class StateGameType(Game1 game) : IGameState {
  private IController keyboardController;

  public ISprite CurrentSprite { get; set; }

  public void Initialize() {
    keyboardController = new GameKeyboardController(game);
  }

  public void LoadContent() {
    CurrentSprite = new FixedSprite(game.GlobalVars.Assets.Textures.MetroTexture, new Vector2(400, 200));
  }

  public void Update(GameTime gameTime) {
    keyboardController.Update(gameTime);
    CurrentSprite.Update(gameTime);
  }

  public void Draw(GameTime gameTime) {
    game.GraphicsDevice.Clear(Color.CornflowerBlue);

    game.SpriteBatch.Begin();
    CurrentSprite.Draw(game.SpriteBatch);
    game.SpriteBatch.End();
  }
}
