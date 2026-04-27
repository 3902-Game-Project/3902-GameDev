using GameProject.Blocks;
using GameProject.Enemies.BossStates;
using GameProject.Enemies.States;
using GameProject.Factories;
using GameProject.Managers;
using GameProject.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

internal class Boss : ABaseEnemy {
  public ILevelManager LevelManager { get; }

  public Boss(Texture2D texture, Vector2 position, ILevelManager levelManager) : base(texture, position, 64f, 128f) {
    LevelManager = levelManager;
    DrawScale = 2.0f;
    FlipOnRightDir = false;
    MaxHealth = 1000;
    Health = 1000;

    CurrentState = new BossIdleState(this);
  }

  // Override TakeDamage to trigger the Hurt animation
  public override void TakeDamage(int damage) {
    if (Health <= 0) return;

    base.TakeDamage(damage);
    if (Health > 0 && CurrentState is not BossHurtState && CurrentState is not BossSpecialAttackState) {
      CurrentState = new BossHurtState(this);
    }
  }

  protected override void TransitionToDeathState() {
    CurrentState = new BossDeathState(this);
  }

  public void FireBullet(int damage) {
    Vector2 direction = Target - Position;
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
  public Vector2 VisualOffset { get; set; } = Vector2.Zero;
  public override void Draw(SpriteBatch spriteBatch) {
    if (CurrentSourceRectangles == null || CurrentSourceRectangles.Count == 0) return;

    Rectangle source = CurrentSourceRectangles[CurrentFrame];
    bool shouldFlip = FlipOnRightDir ? Direction > 0 : Direction <= 0;
    SpriteEffects effect = shouldFlip ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

    Vector2 origin = new(source.Width / 2f, source.Height);
    Color tintColor = DamageFlashTimer > 0 ? Color.Red : Color.White;
    spriteBatch.Draw(Texture, Position + VisualOffset, source, tintColor, 0f, origin, DrawScale, effect, 0f);
  }
}
