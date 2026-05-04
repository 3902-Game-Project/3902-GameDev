using GameProject.Globals;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.ShotgunnerStates;

internal class ShotgunnerAttackState : IEnemyState {
  private readonly Shotgunner shotgunner;
  private double stateTimer = 0.0, animationTimer = 0.0;
  private bool hasFired = false;

  public ShotgunnerAttackState(Shotgunner shotgunner) {
    this.shotgunner = shotgunner;
    this.shotgunner.Velocity = Vector2.Zero;
    this.shotgunner.CurrentSourceRectangles = [
      new(23, 418, 35, 37),
      new(100, 415, 32, 40),
      new(174, 415, 39, 40),
    ];
    this.shotgunner.CurrentFrame = 0;
  }

  public void Update(double deltaTime) {
    animationTimer += deltaTime;
    if (animationTimer >= 0.15 && shotgunner.CurrentFrame < shotgunner.CurrentSourceRectangles.Count - 1) {
      shotgunner.CurrentFrame++;
      animationTimer = 0;
    }

    if (shotgunner.CurrentFrame == shotgunner.CurrentSourceRectangles.Count - 1 && !hasFired) {
      shotgunner.FireSpread(Constants.SHOTGUNNER_DAMAGE);
      hasFired = true;
    }

    stateTimer += deltaTime;
    if (stateTimer > 1.0) shotgunner.CurrentState = new ShotgunnerIdleState(shotgunner);
  }
}
