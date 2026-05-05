using Microsoft.Xna.Framework;

namespace GameProject.Enemies.BossStates;

internal class BossAttack2State : IEnemyState {
  private const double ANIMATION_INTERVAL = 0.15;
  private const double END_ANIMATION_INTERVAL = 0.2;
  private const int BOMB_THROW_FRAME = 4;
  private const int BOMB_STRENGTH = 20;
  private readonly Boss boss;
  private double animationTimer = 0.0;
  private bool hasThrownBomb = false;

  public BossAttack2State(Boss boss) {
    this.boss = boss;
    this.boss.Velocity = Vector2.Zero;

    this.boss.CurrentSourceRectangles = [
      new(56, 317, 56, 50), new(112, 317, 56, 50), new(168, 317, 56, 50),
      new(224, 317, 56, 50), new(280, 317, 56, 50), new(336, 317, 56, 50),
      new(392, 317, 56, 50), new(448, 317, 56, 50),
    ];
    this.boss.CurrentFrame = 0;
  }

  public void Update(double deltaTime) {
    animationTimer += deltaTime;

    if (animationTimer >= ANIMATION_INTERVAL && boss.CurrentFrame < boss.CurrentSourceRectangles.Count - 1) {
      boss.CurrentFrame++;
      animationTimer = 0;
      if (boss.CurrentFrame == BOMB_THROW_FRAME && !hasThrownBomb) {
        Vector2 dirToPlayer = boss.Target - boss.Position;
        boss.Direction = (dirToPlayer.X > 0) ? FacingDirection.Right : FacingDirection.Left;

        boss.ThrowBomb(BOMB_STRENGTH);
        hasThrownBomb = true;
      }
    }

    if (boss.CurrentFrame == boss.CurrentSourceRectangles.Count - 1 && animationTimer >= END_ANIMATION_INTERVAL) {
      boss.CurrentState = new BossIdleState(boss);
    }
  }
}
