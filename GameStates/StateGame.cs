using GameProject.Controllers;
using GameProject.Interfaces;
using GameProject.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameStates;

internal class StateGame : IGameState {
  private IController keyboardController;

  public ISprite CurrentSprite { get; set; }

  public void Initialize() {
    keyboardController = new GameKeyboardController(this);
  }

  public void LoadContent() {
    CurrentSprite = new FixedSprite(Texture, new Vector2(400, 200));
  }

  public void Update(GameTime gameTime) {
    keyboardController.Update(gameTime);
    CurrentSprite.Update(gameTime);
  }

  public void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
    CurrentSprite.Draw(_spriteBatch);
  }
}
