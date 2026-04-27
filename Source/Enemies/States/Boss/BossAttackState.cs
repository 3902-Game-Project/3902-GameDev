using System;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.BossStates;

internal class BossAttackState : IEnemyState {
  private readonly Boss boss;
  private double animationTimer = 0.0;

  private int shotsToFire;
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
      new(289, 148, 48, 46)
        ];
    this.boss.CurrentFrame = 0;
    this.shotsToFire = random.Next(1, 4);
  }

  public void Update(double deltaTime) {
    animationTimer += deltaTime;

    if (animationTimer >= 0.15) {
      animationTimer = 0;
      if (boss.CurrentFrame == 3 && !hasFiredThisLoop) {
        boss.FireBullet(15); // Adjust damage here
        shotsFired++;
        hasFiredThisLoop = true;
      }

      boss.CurrentFrame++;

      // When we try to advance past the final frame (Frame 5)...
      if (boss.CurrentFrame > 5) {
        if (shotsFired < shotsToFire) {
          // If we have more shots to take, loop back to Frame 3!
          boss.CurrentFrame = 3;
          hasFiredThisLoop = false; // Reset the trigger for the next bullet
        } else {
          // If we took all our shots, put the gun away and return to idle
          boss.CurrentState = new BossIdleState(boss);
        }
      }
    }
  }
}
