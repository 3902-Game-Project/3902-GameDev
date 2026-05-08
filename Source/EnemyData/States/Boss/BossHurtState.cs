using Microsoft.Xna.Framework;

namespace GameProject.Enemies.BossStates;

internal class BossHurtState : IEnemyState {
  private const double ANIMATION_INTERVAL = 0.15;

  private readonly Boss boss;
  private double animationTimer = 0.0;

  internal BossHurtState(Boss boss) {
    this.boss = boss;
    this.boss.Velocity = Vector2.Zero;

    this.boss.CurrentSourceRectangles = [
      new(0, 203, 56, 50),
      new(56, 203, 56, 50),
      new(112, 203, 56, 50),
      new(168, 203, 56, 50),
      new(224, 203, 56, 50),
    ];
    this.boss.CurrentFrame = 0;
  }

  internal void Update(double deltaTime) {
    animationTimer += deltaTime;
    if (animationTimer >= ANIMATION_INTERVAL && boss.CurrentFrame < boss.CurrentSourceRectangles.Count - 1) {
      boss.CurrentFrame++;
      animationTimer = 0;
    }
    if (boss.CurrentFrame == boss.CurrentSourceRectangles.Count - 1 && animationTimer >= ANIMATION_INTERVAL) {
      boss.CurrentState = new BossIdleState(boss);
    }
  }
}
