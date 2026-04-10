using GameProject.Interfaces;
using GameProject.Misc;
using Microsoft.Xna.Framework;

namespace GameProject.GameStates;

public class StateTransitionType(Game1 game) : IGameState {
  private ScreenFader screenFader = new(
    game.SpriteBatch,
    game.Assets.Textures.WhitePixel,
    game.Window
  );

  private IGameState toGameState;

  public void Initialize() { }

  public void LoadContent() { }

  public void Update(GameTime gameTime) {
    screenFader.Update(gameTime);
  }

  public void Draw(GameTime gameTime) {
    screenFader.Draw(gameTime);
  }
}
