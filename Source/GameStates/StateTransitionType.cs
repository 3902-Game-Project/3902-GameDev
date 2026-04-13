using System;
using GameProject.Interfaces;
using GameProject.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameStates;

// StateTransitionType is never intended to be the target of game1.ChangeState; it manages fading between 2 states as a result of game1.ChangeState
internal class StateTransitionType(Game1 game) : IGameState {
  private readonly ScreenFader screenFader = new(game.SpriteBatch, game.Window);

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

      case ScreenFader.FadingState.FadedOut: {
          bool nextStateIsSameState = fromGameState == toGameState;

          fromGameState.OnStateEndFadeOut(nextStateIsSameState);
          screenFader.FadeIn();
          toGameState.OnStateStartFadeIn(nextStateIsSameState);
          break;
        }

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

  public void LowLevelDraw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch) {
    switch (screenFader.FadeState) {
      case ScreenFader.FadingState.FadeOut:
        fromGameState.LowLevelDraw(graphicsDevice, spriteBatch);
        break;

      case ScreenFader.FadingState.FadedOut:
      case ScreenFader.FadingState.FadeIn:
      case ScreenFader.FadingState.FadedIn:
        toGameState.LowLevelDraw(graphicsDevice, spriteBatch);
        break;

      default:
        throw new Exception("Unknown fading state value");
    }

    screenFader.LowLevelDraw(graphicsDevice, spriteBatch);
  }

  public void OnStateEnter(bool nextStateIsSameState) {
    screenFader.FadeOut();
  }

  public void OnStateLeave(bool nextStateIsSameState) { }

  public void SetFadingStates(IGameState fromGameState, IGameState toGameState) {
    this.fromGameState = fromGameState;
    this.toGameState = toGameState;
  }

  public void OnStateStartFadeIn(bool nextStateIsSameState) { }

  public void OnStateEndFadeOut(bool nextStateIsSameState) { }
}
