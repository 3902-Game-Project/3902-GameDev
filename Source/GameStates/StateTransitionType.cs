using System;
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

  private IGameState fromGameState;
  private IGameState toGameState;

  public void Initialize() { }

  public void LoadContent() { }

  public void Update(GameTime gameTime) {
    screenFader.Update(gameTime);

    switch (screenFader.FadeState) {
      case ScreenFader.FadingState.FadeOut:
        /* do nothing */
        break;

      case ScreenFader.FadingState.FadedOut:
        screenFader.FadeIn();
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

  public void Draw(GameTime gameTime) {
    screenFader.Draw(gameTime);

    switch (screenFader.FadeState) {
      case ScreenFader.FadingState.FadeOut:
        fromGameState.Draw(gameTime);
        break;

      case ScreenFader.FadingState.FadedOut:
      case ScreenFader.FadingState.FadeIn:
      case ScreenFader.FadingState.FadedIn:
        toGameState.Draw(gameTime);
        break;

      default:
        throw new Exception("Unknown fading state value");
    }
  }

  public void OnStateEnter() {
    screenFader.FadeOut();
  }

  public void OnStateLeave() { }

  public void SetFadingStates(IGameState fromGameState, IGameState toGameState) {
    this.fromGameState = fromGameState;
    this.toGameState = toGameState;
  }
}
