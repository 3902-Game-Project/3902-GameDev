using System;
using GameProject.Globals;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.BossStates;

internal class BossAttackState : IEnemyState {
  private const double ANIMATION_INTERVAL = 0.15;
  private const int FIRE_FRAME = 3;
  private const int LAST_ATTACK_FRAME = 5;
  private const int MIN_SHOTS = 1;
  private const int MAX_SHOTS_EXCLUSIVE = 4;

  private readonly Boss boss;
  private double animationTimer = 0.0;

  private readonly int shotsToFire;
  private int shotsFired = 0;
  private bool hasFiredThisLoop = false;
  private readonly Random random = new();

  public BossAttackState(Boss boss) {
    this.boss = boss;
    this.boss.Velocity = Vector2.Zero;
    this.boss.CurrentSourceRectangles = [
      new(10, 148, 45, 46),
      new(65, 148, 46, 46),
      new(120, 148, 45, 46),
      new(176, 148, 56, 46),
      new(232, 148, 44, 46),
      new(289, 148, 48, 46),
    ];
    this.boss.CurrentFrame = 0;
    shotsToFire = random.Next(MIN_SHOTS, MAX_SHOTS_EXCLUSIVE);
  }

  public void Update(double deltaTime) {
    animationTimer += deltaTime;

    if (animationTimer >= ANIMATION_INTERVAL) {
      animationTimer = 0;
      if (boss.CurrentFrame == FIRE_FRAME && !hasFiredThisLoop) {
        boss.FireBullet(Constants.BOSS_DAMAGE);
        shotsFired++;
        hasFiredThisLoop = true;
      }

      boss.CurrentFrame++;

      // When we try to advance past the final frame (Frame 5)...
      if (boss.CurrentFrame > LAST_ATTACK_FRAME) {
        if (shotsFired < shotsToFire) {
          // If we have more shots to take, loop back to Frame 3!
          boss.CurrentFrame = FIRE_FRAME;
          hasFiredThisLoop = false; // Reset the trigger for the next bullet
        } else {
          // If we took all our shots, put the gun away and return to idle
          boss.CurrentState = new BossIdleState(boss);
        }
      }
    }
  }
}
