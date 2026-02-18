using GameProject.Sprites;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameProject.States {
  public class ShotgunnerIdleState : IShotgunnerState {
    private ShotgunnerSprite shotgunner;
    private double timer;
    private double animationTimer;
    private System.Random random;

    public ShotgunnerIdleState(ShotgunnerSprite shotgunner) {
      this.shotgunner = shotgunner;
      this.random = new System.Random();

      this.shotgunner.Velocity = Vector2.Zero;
      this.shotgunner.CurrentSourceRectangles = new List<Rectangle>
      {
          new Rectangle(21, 339, 32, 39),
          new Rectangle(98, 337, 32, 41),
          new Rectangle(174, 339, 32, 39),
          new Rectangle(251, 341, 32, 37),
      };
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
        if (choice == 0) shotgunner.ChangeState(new ShotgunnerAttackState(shotgunner));
        else shotgunner.ChangeState(new ShotgunnerWanderState(shotgunner));
      }
    }
  }
}
