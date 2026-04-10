using System;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Misc;

internal class ScreenFader(SpriteBatch spriteBatch, Texture2D whitePixelTexture, GameWindow gameWindow) : IGPUpdatable, IGPDrawable {
  public enum FadingState {
    FadeIn,
    FadeOut,
    FadedIn,
    FadedOut,
  };

  private void DrawFadeRectangle(double darkeningIntensity) {
    spriteBatch.Draw(
      texture: whitePixelTexture,
      destinationRectangle: new(0, 0, gameWindow.ClientBounds.Width, gameWindow.ClientBounds.Height),
      color: Color.Black * (float) darkeningIntensity
    );
  }

  private static readonly float FADE_DURATION = 0.2f;
  private double fadeTime = 0.0;

  public FadingState FadeState { get; private set; } = FadingState.FadedOut;

  public void Update(GameTime gameTime) {
    switch (FadeState) {
      case FadingState.FadeIn:
        fadeTime += gameTime.ElapsedGameTime.TotalSeconds;

        if (fadeTime > FADE_DURATION) {
          FadeState = FadingState.FadedIn;
        }
        break;

      case FadingState.FadeOut:
        fadeTime += gameTime.ElapsedGameTime.TotalSeconds;

        if (fadeTime > FADE_DURATION) {
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

  public void Draw(GameTime gameTime) {
    if (FadeState == FadingState.FadeIn || FadeState == FadingState.FadeOut) {
      var fadeProgress = fadeTime / FADE_DURATION;

      if (FadeState == FadingState.FadeIn) {
        DrawFadeRectangle(1.0 - fadeProgress);
      } else {
        DrawFadeRectangle(fadeProgress);
      }
    }
  }

  public void FadeIn() {
    if (FadeState != FadingState.FadedOut) {
      return;
    }

    FadeState = FadingState.FadeIn;
    fadeTime = 0.0;
  }

  public void FadeOut() {
    if (FadeState != FadingState.FadedIn) {
      return;
    }

    FadeState = FadingState.FadeOut;
    fadeTime = 0.0;
  }
}
