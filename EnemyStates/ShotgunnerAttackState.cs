using GameProject.Sprites;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameProject.States {
  public class ShotgunnerAttackState : IShotgunnerState {
    private ShotgunnerSprite shotgunner;
    private double timer;
    private double animationTimer;

    public ShotgunnerAttackState(ShotgunnerSprite shotgunner) {
      this.shotgunner = shotgunner;

      // Lunge in the direction we are currently looking
      this.shotgunner.Velocity = new Vector2(shotgunner.FacingDirection * 0, 0);

      this.shotgunner.CurrentSourceRectangles = new List<Rectangle>
      {
                new Rectangle(9, 142, 13, 16),
            };
      this.shotgunner.CurrentFrame = 0;
    }

    public void Update(GameTime gameTime) {
      float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
      timer += dt;

      animationTimer += dt;
      if (animationTimer >= 0.05) {
        shotgunner.CurrentFrame++;
        if (shotgunner.CurrentFrame >= shotgunner.CurrentSourceRectangles.Count)
          shotgunner.CurrentFrame = 0;
        animationTimer = 0;
      }

      // 2. Movement
      shotgunner.Position += shotgunner.Velocity * dt;

      // 3. Return to Idle
      if (timer > 0.5) {
        shotgunner.ChangeState(new ShotgunnerIdleState(shotgunner));
      }
    }
  }
}
