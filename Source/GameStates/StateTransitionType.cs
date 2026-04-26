using System;
using GameProject.Misc;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameStates;

// StateTransitionType is never intended to be the target of game1.ChangeState; it manages fading between 2 states as a result of game1.ChangeState
internal class StateTransitionType(Game1 game) : IGameState {
  private readonly ScreenFader screenFader = new(game.Window);

  private IGameState fromGameState;
  private IGameState toGameState;

  public void Initialize() { }

  public void LoadContent(ContentManager content) { }

  public void Update(double deltaTime) {
    screenFader.Update(deltaTime);

    switch (screenFader.FadeState) {
      case ScreenFader.FadingState.FadeOut:
        /* do nothing */
        break;

      case ScreenFader.FadingState.FadedOut: {
          bool nextStateIsCurrentState = fromGameState == toGameState;

          fromGameState.OnStateEndFadeOut(nextStateIsCurrentState);
          screenFader.FadeIn();
          toGameState.OnStateStartFadeIn(nextStateIsCurrentState);
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

  public void LowLevelDraw(GraphicsDevice graphicsDevice, RenderTargetTracker renderTargetTracker, SpriteBatch spriteBatch) {
    switch (screenFader.FadeState) {
      case ScreenFader.FadingState.FadeOut:
        fromGameState.LowLevelDraw(graphicsDevice, renderTargetTracker, spriteBatch);
        break;

      case ScreenFader.FadingState.FadedOut:
      case ScreenFader.FadingState.FadeIn:
      case ScreenFader.FadingState.FadedIn:
        toGameState.LowLevelDraw(graphicsDevice, renderTargetTracker, spriteBatch);
        break;

      default:
        throw new Exception("Unknown fading state value");
    }

    screenFader.LowLevelDraw(graphicsDevice, renderTargetTracker, spriteBatch);
  }

  public void OnStateEnter(bool nextStateIsCurrentState) {
    screenFader.FadeOut();
  }

  public void OnStateLeave(bool nextStateIsCurrentState) { }

  public void SetFadingStates(IGameState fromGameState, IGameState toGameState) {
    this.fromGameState = fromGameState;
    this.toGameState = toGameState;
  }

  public void OnStateStartFadeIn(bool nextStateIsCurrentState) { }

  public void OnStateEndFadeOut(bool nextStateIsCurrentState) { }

  public bool NextStateIsCurrentState() {
    return fromGameState == toGameState;
  }
}
