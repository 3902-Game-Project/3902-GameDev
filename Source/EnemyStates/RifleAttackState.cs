using System.Collections.Generic;
using GameProject.Enemies;
using GameProject.Factories;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.States;

public class RifleAttackState : IRiflemanState {
  private RiflemanSprite rifleMan;
  private Game1 game;
  private double stateTimer;
  private double animationTimer;
  private double timePerFrame = 0.15;

  private bool hasFired = false;

  public RifleAttackState(RiflemanSprite rifleMan, Game1 game) {
    this.rifleMan = rifleMan;
    this.game = game;

    this.rifleMan.Velocity = Vector2.Zero;

    this.rifleMan.CurrentSourceRectangles = new List<Rectangle> {
      new(198, 91, 21, 27),
      new(260, 91, 22, 27),
      new(323, 89, 23, 29),
    };
    this.rifleMan.CurrentFrame = 0;
  }

  public void Update(GameTime gameTime) {
    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

    animationTimer += dt;
    if (animationTimer >= timePerFrame) {
      if (rifleMan.CurrentFrame < rifleMan.CurrentSourceRectangles.Count - 1) {
        rifleMan.CurrentFrame++;
        animationTimer = 0;
      }
    }

    if (rifleMan.CurrentFrame == rifleMan.CurrentSourceRectangles.Count - 1 && !hasFired) {
      FireBullet();
      hasFired = true;
    }

    stateTimer += dt;
    if (stateTimer > 1.0) {
      rifleMan.ChangeState(new RifleIdleState(rifleMan, game));
    }
  }

  private void FireBullet() {
    Vector2 bulletDirection = new Vector2(rifleMan.FacingDirection, 0f);

    // Calculate a spawn point so it comes out of the gun barrel, not his feet.
    Vector2 spawnOffset = new Vector2(rifleMan.FacingDirection * 15f, -33f);
    Vector2 spawnPosition = rifleMan.Position + spawnOffset;

    // Create the bullet (Velocity: 300f, Lifetime: 2 seconds)
    IProjectile bullet = ProjectileFactory.Instance.CreateBullet(spawnPosition, bulletDirection, 300f, 2f);

    game.StateGame.LevelManager.CurrentLevel.ProjectileManager.AddProjectile(bullet);
  }
}
