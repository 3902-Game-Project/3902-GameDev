using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.States;

internal abstract class AEnemyMoveState : IEnemyState {
  private double wanderTimer;
  private readonly double wanderDuration;
  private double animationTimer = 0.0;
  private readonly float speed;
  private readonly bool lockYAxis;
  
  protected readonly ABaseEnemy enemy;
  protected readonly Random random = new();

  public AEnemyMoveState(ABaseEnemy enemy, List<Rectangle> frames, float speed, bool lockYAxis = false) {
    this.enemy = enemy;
    this.speed = speed;
    this.lockYAxis = lockYAxis;
    this.enemy.CurrentSourceRectangles = frames;
    this.enemy.CurrentFrame = 0;

    wanderTimer = 0.0;
    wanderDuration = 1.0 + (random.NextDouble() * 2.0);
  }

  public virtual void Update(double deltaTime) {
    animationTimer += deltaTime;
    if (animationTimer >= 0.2) {
      enemy.CurrentFrame = (enemy.CurrentFrame + 1) % enemy.CurrentSourceRectangles.Count;
      animationTimer = 0;
    }

    enemy.Position += enemy.Velocity * ((float) deltaTime);

    enemy.Navigate(speed);

    if (enemy.Position.X < 0 || enemy.Position.X > 800) {
      enemy.Velocity = new Vector2(-enemy.Velocity.X, enemy.Velocity.Y);
      enemy.Direction = enemy.Velocity.X > 0 ? FacingDirection.Right : FacingDirection.Left;
    }
    if (!lockYAxis && (enemy.Position.Y < 0 || enemy.Position.Y > 480)) {
      enemy.Velocity = new Vector2(enemy.Velocity.X, -enemy.Velocity.Y);
    }

    wanderTimer += deltaTime;
    if (wanderTimer >= wanderDuration) TransitionToNextState();
  }

  protected abstract void TransitionToNextState();
}
