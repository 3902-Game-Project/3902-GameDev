using System;
using System.Collections.Generic;
using System.Diagnostics;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameProject.Misc;

public class Level : ILevel {
  public enum FadingState {
    FadeIn,
    Active,
    FadeOut,
  };

  private static float FADE_DURATION = 0.2f;

  private Game1 game;
  private List<IBlock> nonCollidableBlocks; // for non-collidable collidableBlocks -Aaron
  private List<IBlock> collidableBlocks;
  private List<IEnemy> enemies;
  private List<IWorldPickup> pickups;

  private FadingState fadeState = FadingState.FadeIn;
  private double fadeTime = 0.0;

  public List<IBlock> CollidableBlocks => collidableBlocks;
  public List<IEnemy> Enemies => enemies;
  public Vector2 PlayerPosition { get; private set; }

  public Level(
    Game1 game,
    List<IBlock> nonCollidableBlocks, // for non-collidable collidableBlocks -Aaron
    List<IBlock> collidableBlocks,
    List<IEnemy> enemies,
    List<IWorldPickup> pickups,
    Vector2 playerPosition
  ) {
    this.game = game;
    this.nonCollidableBlocks = nonCollidableBlocks;
    this.collidableBlocks = collidableBlocks;
    this.enemies = enemies;
    this.pickups = pickups;
    PlayerPosition = playerPosition;
  }

  public void Initialize() { }

  public void LoadContent(ContentManager content) { }

  public void Update(GameTime gameTime) {
    switch (fadeState) {
      case FadingState.FadeIn:
        fadeTime += gameTime.ElapsedGameTime.TotalSeconds;

        if (fadeTime > FADE_DURATION) {
          fadeState = FadingState.Active;
        }
        break;

      case FadingState.Active:
        foreach (var nonCollidableBlocks in nonCollidableBlocks) {
          nonCollidableBlocks.Update(gameTime);
        }

        foreach (var collidableBlock in collidableBlocks) {
          collidableBlock.Update(gameTime);
        }

        foreach (var enemy in enemies) {
          enemy.Update(gameTime);
        }

        foreach (var pickup in pickups) {
          pickup.Update(gameTime);
        }
        break;

      case FadingState.FadeOut:
        fadeTime += gameTime.ElapsedGameTime.TotalSeconds;

        if (fadeTime > FADE_DURATION) {
          game.StateGame.LevelManager.CompleteLevelSwitch();
        }
        break;

      default:
        throw new Exception("Unknown fading state value");
    }
  }

  public void Draw(GameTime gameTime) {
    foreach (var nonCollidableBlock in nonCollidableBlocks) {
      nonCollidableBlock.Draw(game.SpriteBatch);
    }

    foreach (var collidableBlock in collidableBlocks) {
      collidableBlock.Draw(game.SpriteBatch);
    }

    foreach (var enemy in enemies) {
      enemy.Draw(game.SpriteBatch);
    }

    foreach (var pickup in pickups) {
      pickup.Draw(game.SpriteBatch);
    }
  }

  public void AddPickup(IWorldPickup pickup) {
    pickups.Add(pickup);
  }

  public void FadeIn() {
    fadeState = FadingState.FadeIn;
    fadeTime = 0.0;
  }

  public void FadeOut() {
    fadeState = FadingState.FadeOut;
    fadeTime = 0.0;
  }

  public bool IsFadingOut() {
    return fadeState == FadingState.FadeOut;
  }
}
