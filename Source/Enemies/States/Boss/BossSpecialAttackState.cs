using Microsoft.Xna.Framework;

namespace GameProject.Enemies.BossStates;

internal class BossSpecialAttackState : IEnemyState {
  private readonly Boss boss;
  private double animationTimer = 0.0;
  private bool hasSpawnedBombs = false;

  public BossSpecialAttackState(Boss boss) {
    this.boss = boss;
    this.boss.Velocity = Vector2.Zero; // Lock him in place

    this.boss.CurrentSourceRectangles = [
      new(56, 258, 56, 54), new(112, 258, 56, 54), new(168, 258, 56, 54),
      new(224, 258, 56, 54), new(280, 258, 56, 54), new(336, 258, 56, 54),
      new(392, 258, 56, 54), new(448, 258, 56, 54), new(504, 258, 56, 54),
    ];
    this.boss.CurrentFrame = 0;
  }

  public void Update(double deltaTime) {
    animationTimer += deltaTime;

    if (animationTimer >= 0.15 && boss.CurrentFrame < boss.CurrentSourceRectangles.Count - 1) {
      boss.CurrentFrame++;
      animationTimer = 0;

      // --- TRIGGER THE ARENA BOMBS ON FRAME 5 ---
      if (boss.CurrentFrame == 5 && !hasSpawnedBombs) {
        boss.SpawnArenaBombs();
        hasSpawnedBombs = true;
      }
    }

    // Return to idle once the animation completes
    if (boss.CurrentFrame == boss.CurrentSourceRectangles.Count - 1 && animationTimer >= 0.2) {
      boss.CurrentState = new BossIdleState(boss);
    }
  }
}
