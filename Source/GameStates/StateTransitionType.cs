using System;
using GameProject.Interfaces;
using GameProject.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameStates;

// StateTransitionType is never intended to be the target of game1.ChangeState; it manages fading between 2 states as a result of game1.ChangeState
public class StateTransitionType(Game1 game) : IGameState {
  private readonly ScreenFader screenFader = new(
    game.SpriteBatch,
    game.Assets.Textures.WhitePixel,
    game.Window
  );

  private IGameState fromGameState;
  private IGameState toGameState;

  public void Initialize() { }

  public void LoadContent(ContentManager content) { }

  public void Update(GameTime gameTime) {
    screenFader.Update(gameTime);

    switch (screenFader.FadeState) {
      case ScreenFader.FadingState.FadeOut:
        /* do nothing */
        break;

      case ScreenFader.FadingState.FadedOut:
        fromGameState.OnStateEndFadeOut();
        screenFader.FadeIn();
        toGameState.OnStateStartFadeIn();
        break;

      case ScreenFader.FadingState.FadeIn:
        /* do nothing */
        break;

      case ScreenFader.FadingState.FadedIn:
        game.ChangeStateWithoutFading(toGameState);
        break;

      default:
        throw new Exception("Unknown fading state value");
    }
  }

  public void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch) {
    switch (screenFader.FadeState) {
      case ScreenFader.FadingState.FadeOut:
        fromGameState.Draw(graphicsDevice, spriteBatch);
        break;

      case ScreenFader.FadingState.FadedOut:
      case ScreenFader.FadingState.FadeIn:
      case ScreenFader.FadingState.FadedIn:
        toGameState.Draw(graphicsDevice, spriteBatch);
        break;

      default:
        throw new Exception("Unknown fading state value");
    }

    screenFader.Draw(graphicsDevice, spriteBatch);
  }

  public void OnStateEnter() {
    screenFader.FadeOut();
  }

  public void OnStateLeave() { }

  public void SetFadingStates(IGameState fromGameState, IGameState toGameState) {
    this.fromGameState = fromGameState;
    this.toGameState = toGameState;
  }

  public void OnStateStartFadeIn() { }

  public void OnStateEndFadeOut() { }
}
