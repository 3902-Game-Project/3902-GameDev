using Microsoft.Xna.Framework;

namespace GameProject.Enemies.BatStates;

internal class BatIdleState : IBatState {
  private readonly Bat bat;
  private double timer;
  private readonly System.Random random;
  private double animationTimer;

  public BatIdleState(Bat bat) {
    this.bat = bat;
    random = new System.Random();

    this.bat.Velocity = Vector2.Zero;

    this.bat.CurrentSourceRectangles = [
      //new Rectangle(3, 20, 25, 11),
      new(35, 5, 27, 22),
      new(66, 6, 29, 15),
      new(97, 1, 31, 21),
    ];
    this.bat.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) {
    animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
    if (animationTimer > 0.2) {
      bat.CurrentFrame++;
      if (bat.CurrentFrame >= bat.CurrentSourceRectangles.Count) {
        bat.CurrentFrame = 0;
      }
      animationTimer = 0;
    }
    timer += gameTime.ElapsedGameTime.TotalSeconds;
    if (timer > 2.0) {
      bat.ChangeState(new BatMoveState(bat));
    }
  }
}
