using Microsoft.Xna.Framework;

namespace GameProject.Enemies.RiflemanStates;

internal class RifleAttackState : IEnemyState {
  private readonly Rifleman rifleMan;
  private double stateTimer = 0.0, animationTimer = 0.0;
  private bool hasFired = false;

  public RifleAttackState(Rifleman rifleMan) {
    this.rifleMan = rifleMan;
    this.rifleMan.Velocity = Vector2.Zero;
    this.rifleMan.CurrentSourceRectangles = [new(198, 91, 21, 27), new(260, 91, 22, 27), new(323, 89, 23, 29)];
    this.rifleMan.CurrentFrame = 0;
  }

  public void Update(double deltaTime) {
    animationTimer += deltaTime;
    if (animationTimer >= 0.15 && rifleMan.CurrentFrame < rifleMan.CurrentSourceRectangles.Count - 1) {
      rifleMan.CurrentFrame++;
      animationTimer = 0;
    }

    if (rifleMan.CurrentFrame == rifleMan.CurrentSourceRectangles.Count - 1 && !hasFired) {
      rifleMan.FireProjectile(80);
      hasFired = true;
    }

    stateTimer += deltaTime;
    if (stateTimer > 1.0) rifleMan.CurrentState = new RifleIdleState(rifleMan);
  }
}
