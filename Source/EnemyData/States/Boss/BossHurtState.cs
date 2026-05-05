using Microsoft.Xna.Framework;

namespace GameProject.Enemies.BossStates;

internal class BossHurtState : IEnemyState {
  private readonly Boss boss;
  private double animationTimer = 0.0;

  public BossHurtState(Boss boss) {
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

  public void Update(double deltaTime) {
    animationTimer += deltaTime;
    if (animationTimer >= 0.15 && boss.CurrentFrame < boss.CurrentSourceRectangles.Count - 1) {
      boss.CurrentFrame++;
      animationTimer = 0;
    }
    if (boss.CurrentFrame == boss.CurrentSourceRectangles.Count - 1 && animationTimer >= 0.15) {
      boss.CurrentState = new BossIdleState(boss);
    }
  }
}
