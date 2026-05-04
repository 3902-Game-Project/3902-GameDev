using System;
using System.Collections.Generic;
using GameProject.Blocks;
using GameProject.Collisions;
using GameProject.Collisions.Shapes;
using GameProject.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

internal abstract class ABaseEnemy(
  Texture2D texture,
  Vector2 position,
  float colliderWidth = Constants.BASE_ENEMY_WIDTH,
  float colliderHeight = Constants.BASE_ENEMY_HEIGHT,
  bool invulnerable = false
) : IEnemy {
  public static event Action<ABaseEnemy> OnDeath;

  protected float DrawScale { get; set; } = 1f;
  protected bool FlipOnRightDir { get; set; } = true;

  protected virtual void DropLoot() { }

  protected virtual void TransitionToDeathState() { }

  protected void UpdateCollider() {
    if (Collider != null) {
      Collider.Position = Position + new Vector2(0, -Collider.Height / 2f);
    }
  }

  protected virtual void ChangeDirection() {
    Vector2 delta = Target - Position;
    if (delta == Vector2.Zero) {
      Velocity = Vector2.Zero;
      return;
    }

    float absX = Math.Abs(delta.X);
    float absY = Math.Abs(delta.Y);

    if (absX >= absY) {
      Direction = delta.X >= 0 ? FacingDirection.Right : FacingDirection.Left;
    } else {
      Direction = delta.Y >= 0 ? FacingDirection.Down : FacingDirection.Up;
    }
  }

  protected virtual void Die() {
    DropLoot();
    OnDeath?.Invoke(this);
    TransitionToDeathState();
  }

  public Texture2D Texture { get; protected set; } = texture;
  public Vector2 Position { get; set; } = position;
  public Vector2 Velocity { get; set; }
  public FacingDirection Direction { get; set; } = FacingDirection.Right;

  public Vector2 Target { get; set; } = position;

  public List<Rectangle> CurrentSourceRectangles { get; set; } = [];
  public int CurrentFrame { get; set; }
  public IShape Shape => Collider;
  public BoxCollider Collider { get; private set; } = new BoxCollider(colliderWidth, colliderHeight, position);
  public Layer Layer { get; } = Layer.Enemies;
  public Layer Mask { get; } = Layer.Player;
  public int Health { get; set; } = 100;
  public int MaxHealth { get; set; } = 100;
  public double DamageFlashTimer { get; protected set; }
  public bool Invulnerable { get; } = invulnerable;

  public IEnemyState CurrentState { get; set; }

  public Rectangle BoundingBox => new((int) (Position.X - Collider.Width / 2), (int) (Position.Y - Collider.Height / 2), (int) Collider.Width, (int) Collider.Height);

  public virtual void OnCollision(CollisionInfo info) {
    if (info.Collider is IBlock) {
      if (info.Side == CollisionSide.Left || info.Side == CollisionSide.Right) {
        Velocity = new Vector2(0, Velocity.Y);
      } else if (info.Side == CollisionSide.Top || info.Side == CollisionSide.Bottom) {
        Velocity = new Vector2(Velocity.X, 0);
      }
      Position = CollisionHelper.GetNudgedPosition(info, Position, 2f);
      UpdateCollider();
    }
  }

  public virtual void Draw(SpriteBatch spriteBatch) {
    if (CurrentSourceRectangles == null || CurrentSourceRectangles.Count == 0) return;

    Rectangle source = CurrentSourceRectangles[CurrentFrame];
    bool shouldFlip = FlipOnRightDir ? Direction > 0 : Direction <= 0;
    SpriteEffects effect = shouldFlip ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

    Vector2 origin = new(source.Width / 2f, source.Height);
    Color tintColor = DamageFlashTimer > 0 ? Color.Red : Color.White;

    spriteBatch.Draw(Texture, Position, source, tintColor, 0f, origin, DrawScale, effect, 0f);
  }

  public virtual void Update(double deltaTime) {
    if (DamageFlashTimer > 0) {
      DamageFlashTimer -= deltaTime;
    }
    UpdateCollider();
    CurrentState?.Update(deltaTime);
    if (Position.X < 0) {
      Position = new Vector2(0, Position.Y);
    }

    ChangeDirection();
  }

  public virtual void TakeDamage(int damage) {
    if (Invulnerable) {
      return;
    }

    if (Health <= 0) {
      return;
    }

    bool wasAlive = Health > 0;
    Health -= damage;
    DamageFlashTimer = Constants.ENEMY_DAMAGE_FLASH_DURATION;

    if (wasAlive && Health <= 0) {
      Die();
    }
  }

  public virtual void Kill() {
    Health = 0;
    Die();
  }

  public virtual void Navigate(float speed) {
    Vector2 delta = Target - Position;
    if (delta == Vector2.Zero) {
      Velocity = Vector2.Zero;
      return;
    }

    Vector2 direction = Vector2.Normalize(delta);
    Velocity = direction * speed;
  }
}
