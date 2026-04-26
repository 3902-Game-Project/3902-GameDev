using Microsoft.Xna.Framework;

namespace GameProject.Enemies.BossStates;

internal class BossAttackState : IEnemyState {
  private readonly Boss boss;
  private double stateTimer = 0.0, animationTimer = 0.0;
  private bool hasAttacked = false;

  public BossAttackState(Boss boss) {
    this.boss = boss;
    this.boss.Velocity = Vector2.Zero;
    // Placeholder attack frames
    this.boss.CurrentSourceRectangles = [new(0, 128, 64, 64), new(64, 128, 64, 64), new(128, 128, 64, 64)];
    this.boss.CurrentFrame = 0;
  }

  public void Update(double deltaTime) {
    animationTimer += deltaTime;
    if (animationTimer >= 0.2 && boss.CurrentFrame < boss.CurrentSourceRectangles.Count - 1) {
      boss.CurrentFrame++;
      animationTimer = 0;
    }

    // Trigger the attack logic on the last frame
    if (boss.CurrentFrame == boss.CurrentSourceRectangles.Count - 1 && !hasAttacked) {
      // Placeholder: You will add the actual attack/projectile logic here later
      hasAttacked = true;
    }

    stateTimer += deltaTime;
    // Return to idle after the attack finishes
    if (stateTimer > 1.0) {
      boss.CurrentState = new BossIdleState(boss);
    }
  }
}
