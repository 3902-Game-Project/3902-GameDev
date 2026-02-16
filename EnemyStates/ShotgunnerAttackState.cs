using GameProject.Sprites;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameProject.States {
  public class ShotgunnerAttackState : IShotgunnerState {
    private ShotgunnerSprite shotgunner;
    private double stateTimer;
    private double animationTimer;
    private double timePerFrame = 0.15;

    public ShotgunnerAttackState(ShotgunnerSprite shotgunner) {
      this.shotgunner = shotgunner;

      this.shotgunner.Velocity = Vector2.Zero;

      this.shotgunner.CurrentSourceRectangles = new List<Rectangle>
      {
                new Rectangle(10, 175, 14, 15),
                new Rectangle(42, 174, 13, 16),
                new Rectangle(73, 174, 16, 16)
            };
      this.shotgunner.CurrentFrame = 0;
    }

    public void Update(GameTime gameTime) {
      float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
      animationTimer += dt;
      if (animationTimer >= timePerFrame) {
        if (shotgunner.CurrentFrame < shotgunner.CurrentSourceRectangles.Count - 1) {
          shotgunner.CurrentFrame++;
          animationTimer = 0; 
        }
      }
      stateTimer += dt;
      if (stateTimer > 1.0) {
        shotgunner.ChangeState(new ShotgunnerIdleState(shotgunner));
      }
    }
  }
}
