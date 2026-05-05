using System;
using GameProject.Enemies.BossStates;
using GameProject.Globals;
using GameProject.Level;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

internal class Boss : ABaseEnemy {
  public ILevelManager LevelManager { get; }

  public bool PhaseTwoTriggered { get; set; } = false;
  private readonly Random random = new();

  public Boss(Texture2D texture, Vector2 position, ILevelManager levelManager) : base(texture, position, colliderHeight: Constants.BASE_ENEMY_HEIGHT * 2.0f) {
    LevelManager = levelManager;
    DrawScale = 2.0f;
    FlipWhenFacingRightUpDown = false;
    MaxHealth = 1000;
    Health = 1000;

    CurrentState = new BossIdleState(this);
  }

  public override void TakeDamage(int damage) {
    if (Health <= 0) return;

    base.TakeDamage(damage);

    // --- PHASE TRANSITION LOGIC ---
    if (Health > 0) {
      if (Health <= MaxHealth / 2 && !PhaseTwoTriggered) {
        PhaseTwoTriggered = true;
        CurrentState = new BossSpecialAttackState(this);
        return;
      }

      if (CurrentState is not BossHurtState && CurrentState is not BossSpecialAttackState) {
        if (random.Next(100) < 20) {
          CurrentState = new BossHurtState(this);
        }
      }
    }
  }

  public void FireBullet(int damage) {
    Vector2 direction = Target - Position;

    // Force the boss to turn and face the player right as he shoots
    if (direction.X > 0) {
      Direction = FacingDirection.Right;
    } else if (direction.X < 0) {
      Direction = FacingDirection.Left;
    }

    if (direction != Vector2.Zero) {
      direction.Normalize();
    } else {
      direction = new Vector2((Direction == FacingDirection.Right) ? 1 : -1, 0);
    }

    float gunOffsetX = 35f;
    float gunOffsetY = -25f;

    Vector2 spawnPosition = Position;

    if (Direction == FacingDirection.Right) {
      spawnPosition += new Vector2(gunOffsetX, gunOffsetY);
    } else {
      spawnPosition += new Vector2(-gunOffsetX, gunOffsetY);
    }

    IProjectile bullet = ProjectileFactory.Instance.CreateBullet(spawnPosition, direction, 350f, 0.8f, damage);
    if (bullet is BulletDefault b) b.IsPlayerShot = false;

    LevelManager.CurrentLevel.ProjectileManager.Add(bullet);
  }

  public void ThrowBomb(int damage) {
    float offsetX = 40f;
    float offsetY = -45f;

    Vector2 spawnPosition = Position;
    Vector2 tossDirection; // implicitly assigned Vector2.Zero, reassured by IDE this is the case

    if (Direction == FacingDirection.Right) {
      spawnPosition += new Vector2(offsetX, offsetY);
      tossDirection = new Vector2(1, 0);
    } else {
      spawnPosition += new Vector2(-offsetX, offsetY);
      tossDirection = new Vector2(-1, 0);
    }

    IProjectile bomb = ProjectileFactory.Instance.CreateBossBomb(spawnPosition, tossDirection, damage);
    LevelManager.CurrentLevel.ProjectileManager.Add(bomb);
  }

  public void SpawnArenaBombs() {
    // Adjust these grid coordinates to fit your actual room size!
    int[] xPositions = [150, 400, 650];
    int[] yPositions = [150, 300, 450];

    Random rand = new();
    int safeCol = rand.Next(xPositions.Length);
    int safeRow = rand.Next(yPositions.Length);

    for (int x = 0; x < xPositions.Length; x++) {
      for (int y = 0; y < yPositions.Length; y++) {

        if (x == safeCol && y == safeRow) continue;

        Vector2 spawnPos = new(xPositions[x], yPositions[y]);

        IProjectile arenaBomb = ProjectileFactory.Instance.CreateBossBomb(spawnPos, Vector2.Zero, 25);
        LevelManager.CurrentLevel.ProjectileManager.Add(arenaBomb);
      }
    }
  }

  public override void Draw(SpriteBatch spriteBatch) {
    if (CurrentSourceRectangles == null || CurrentSourceRectangles.Count == 0) return;

    Rectangle source = CurrentSourceRectangles[CurrentFrame];

    // --- THE MAGIC MATH FIX FOR JITTER ---
    int cellIndex = source.X / 56;
    float trueCenterX = (cellIndex * 56) + 28;
    float originX = trueCenterX - source.X;
    Vector2 origin = new(originX, source.Height);
    // -------------------------------------

    Color tintColor = DamageFlashTimer > 0 ? Color.Red : Color.White;

    spriteBatch.Draw(Texture, Position, source, tintColor, 0f, origin, DrawScale, GetFlipEffect(), 0f);
  }

  protected override void TransitionToDeathState() {
    CurrentState = new BossDeathState(this);
  }
}
