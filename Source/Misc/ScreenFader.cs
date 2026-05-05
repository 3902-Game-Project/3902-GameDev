using System;
using GameProject.GlobalInterfaces;
using GameProject.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Misc;

internal class ScreenFader(GameWindow gameWindow) : ITemporalUpdatable, ILowLevelDrawable {
  internal enum FadingState {
    FadeIn,
    FadeOut,
    FadedIn,
    FadedOut,
  };

  private void DrawFadeRectangle(SpriteBatch spriteBatch, double darkeningIntensity) {
    spriteBatch.Draw(
      texture: TextureStore.Instance.WhitePixel,
      destinationRectangle: new(0, 0, gameWindow.ClientBounds.Width, gameWindow.ClientBounds.Height),
      color: Color.Black * (float) darkeningIntensity
    );
  }

  private double fadeTime = 0.0;

  public FadingState FadeState { get; private set; } = FadingState.FadedIn;

  public void Update(double deltaTime) {
    switch (FadeState) {
      case FadingState.FadeIn:
        fadeTime += deltaTime;

        if (fadeTime > Constants.SCREEN_FADE_DURATION) {
          FadeState = FadingState.FadedIn;
        }
        break;

      case FadingState.FadeOut:
        fadeTime += deltaTime;

        if (fadeTime > Constants.SCREEN_FADE_DURATION) {
          FadeState = FadingState.FadedOut;
        }
        break;

      case FadingState.FadedIn:
      case FadingState.FadedOut:
        /* do nothing */
        break;

      default:
        throw new Exception("Unknown fading state value");
    }
  }

  public void LowLevelDraw(LowLevelDrawParams drawData) {
    var fadeProgress = fadeTime / Constants.SCREEN_FADE_DURATION;

    drawData.SpriteBatch.Begin();

    switch (FadeState) {
      case FadingState.FadeIn:
        DrawFadeRectangle(drawData.SpriteBatch, 1.0 - fadeProgress);
        break;

      case FadingState.FadeOut:
        DrawFadeRectangle(drawData.SpriteBatch, fadeProgress);
        break;

      case FadingState.FadedIn:
        /* draw nothing */
        break;

      case FadingState.FadedOut:
        DrawFadeRectangle(drawData.SpriteBatch, 1.0);
        break;

      default:
        throw new Exception("Unknown fading state value");
    }

    drawData.SpriteBatch.End();
  }

  public void FadeIn() {
    if (FadeState != FadingState.FadedOut) {
      throw new InvalidOperationException("Cannot fade in if not faded out");
    }

    FadeState = FadingState.FadeIn;
    fadeTime = 0.0;
  }

  public void FadeOut() {
    if (FadeState != FadingState.FadedIn) {
      throw new InvalidOperationException("Cannot fade out if not faded in");
    }

    FadeState = FadingState.FadeOut;
    fadeTime = 0.0;
  }
}
