using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.States;

internal class GenericDeathState : IEnemyState {
  private readonly ABaseEnemy enemy;
  private readonly double timePerFrame;
  private readonly double timeToHoldLastFrame;
  private double animationTimer;
  private double deadHoldTimer;
  private bool isAnimationFinished = false;

  public GenericDeathState(ABaseEnemy enemy, List<Rectangle> frames, double timePerFrame = 0.15, double holdTime = 1.5) {
    this.enemy = enemy;
    this.timePerFrame = timePerFrame;
    timeToHoldLastFrame = holdTime;

    this.enemy.Velocity = Vector2.Zero;
    this.enemy.CurrentSourceRectangles = frames;
    this.enemy.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) {
    float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;

    if (!isAnimationFinished) {
      animationTimer += dt;
      if (animationTimer >= timePerFrame) {
        if (enemy.CurrentFrame < enemy.CurrentSourceRectangles.Count - 1) {
          enemy.CurrentFrame++;
          animationTimer = 0;
        } else {
          isAnimationFinished = true;
        }
      }
    } else {
      deadHoldTimer += dt;
      if (deadHoldTimer >= timeToHoldLastFrame) {
        enemy.CurrentSourceRectangles.Clear();
      }
    }
  }
}
