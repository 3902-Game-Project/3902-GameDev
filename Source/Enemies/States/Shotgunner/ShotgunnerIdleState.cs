using Microsoft.Xna.Framework;

namespace GameProject.Enemies.ShotgunnerStates;

internal class ShotgunnerIdleState : IShotgunnerState {
  private readonly ShotgunnerSprite shotgunner;
  private readonly ILevelManager levelManager;
  private double timer;
  private double animationTimer;
  private readonly System.Random random;

  public ShotgunnerIdleState(ShotgunnerSprite shotgunner, ILevelManager levelManager) {
    this.shotgunner = shotgunner;
    this.levelManager = levelManager;
    random = new System.Random();

    this.shotgunner.Velocity = Vector2.Zero;
    this.shotgunner.CurrentSourceRectangles = [
      new(21, 339, 32, 39),
      new(98, 337, 32, 41),
      new(174, 339, 32, 39),
      new(251, 341, 32, 37),
    ];
    this.shotgunner.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) {
    animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
    if (animationTimer > 0.2) {
      shotgunner.CurrentFrame++;
      if (shotgunner.CurrentFrame >= shotgunner.CurrentSourceRectangles.Count) {
        shotgunner.CurrentFrame = 0;
      }
      animationTimer = 0;
    }
    timer += gameTime.ElapsedGameTime.TotalSeconds;

    if (timer > 1.0) {
      int choice = random.Next(0, 2);
      if (choice == 0) {
        shotgunner.ChangeState(new ShotgunnerAttackState(shotgunner, levelManager));
      } else {
        shotgunner.ChangeState(new ShotgunnerWanderState(shotgunner, levelManager));
      }
    }
  }
}
