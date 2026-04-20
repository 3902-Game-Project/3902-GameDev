using GameProject.Managers;
using Microsoft.Xna.Framework;

namespace GameProject.Enemies.RiflemanStates;

internal class RifleIdleState : IRiflemanState {
  private readonly Rifleman rifle;
  private readonly ILevelManager levelManager;
  private double timer;
  private double animationTimer;
  private readonly System.Random random;

  public RifleIdleState(Rifleman rifle, ILevelManager levelManager) {
    this.rifle = rifle;
    this.levelManager = levelManager;
    random = new System.Random();

    this.rifle.Velocity = Vector2.Zero;

    this.rifle.CurrentSourceRectangles = [
      new(71, 130, 23, 28),
      new(134, 130, 23, 28),
      new(196, 130, 23, 28),
      new(259, 130, 23, 28),
    ];
    this.rifle.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) {
    animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
    if (animationTimer > 0.2) {
      rifle.CurrentFrame++;
      if (rifle.CurrentFrame >= rifle.CurrentSourceRectangles.Count) {
        rifle.CurrentFrame = 0;
      }
      animationTimer = 0;
    }
    timer += gameTime.ElapsedGameTime.TotalSeconds;

    if (timer > 1.0) {
      int choice = random.Next(0, 2);
      if (choice == 0) {
        rifle.ChangeState(new RifleAttackState(rifle, levelManager));
      } else {
        rifle.ChangeState(new RifleWanderState(rifle, levelManager));
      }
    }
  }
}
