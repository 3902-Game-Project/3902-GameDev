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

  protected Texture2D Texture { get; } = texture;
  protected float DrawScale { get; set; } = 1.0f;
  protected bool FlipWhenFacingRightUpDown { get; set; } = true;
  protected int MaxHealth { get; set; } = 100;

  protected virtual void DropLoot() { }

  protected virtual void TransitionToDeathState() { }

  protected void UpdateCollider() {
    if (Collider != null) {
      Collider.Position = Position + new Vector2(0.0f, -Collider.Height * 0.5f);
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

  protected SpriteEffects GetFlipEffect() {
    // Assume FlipWhenFacingRightUpDown = true
    bool shouldFlip = Direction switch {
      FacingDirection.Left => false,
      FacingDirection.Right => true,
      FacingDirection.Up => true,
      FacingDirection.Down => true,
      _ => throw new NotImplementedException("FacingDirection value not handled"),
    };

    // Invert result if FlipWhenFacingRightUpDown = false
    if (!FlipWhenFacingRightUpDown) {
      shouldFlip = !shouldFlip;
    }

    return shouldFlip ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
  }

  protected virtual Vector2 GetDrawOrigin(Rectangle source) {
    return new(source.Width * 0.5f, source.Height);
  }

  public Vector2 Position { get; set; } = position;
  public Vector2 Velocity { get; set; }
  public FacingDirection Direction { get; set; } = FacingDirection.Right;

  public Vector2 Target { get; set; } = position;

  public List<Rectangle> CurrentSourceRectangles { get; set; } = [];
  public int CurrentFrame { get; set; }
  public IShape Shape => Collider;
  public BoxCollider Collider { get; private set; } = new BoxCollider(colliderWidth, colliderHeight, position);
  public Layer Layer { get; } = Layer.Enemies;
  public int Health { get; set; } = 100;
  public double DamageFlashTimer { get; protected set; }
  public bool Invulnerable { get; } = invulnerable;

  public IEnemyState CurrentState { get; set; }

  public virtual void OnCollision(CollisionInfo info) {
    if (info.Collider is IBlock) {
      if (info.Side == CollisionSide.Left || info.Side == CollisionSide.Right) {
        Velocity = new Vector2(0.0f, Velocity.Y);
      } else if (info.Side == CollisionSide.Top || info.Side == CollisionSide.Bottom) {
        Velocity = new Vector2(Velocity.X, 0.0f);
      }
      Position = CollisionHelper.GetNudgedPosition(info, Position, 2.0f);
      UpdateCollider();
    }
  }

  public virtual void Draw(SpriteBatch spriteBatch) {
    // Draw enemy

    if (CurrentSourceRectangles != null && CurrentSourceRectangles.Count > 0) {
      Rectangle source = CurrentSourceRectangles[CurrentFrame];

      Vector2 origin = GetDrawOrigin(source);
      Color tintColor = DamageFlashTimer > 0 ? Color.Red : Color.White;

      spriteBatch.Draw(Texture, Position, source, tintColor, 0.0f, origin, DrawScale, GetFlipEffect(), 0.0f);
    }

    // Draw health bar

    if (!Invulnerable && Health > 0) {
      float enemyHealthPercent = MathHelper.Clamp((float) Health / MaxHealth, 0.0f, 1.0f);
      float scaleWidth = TextureStore.Instance.HealthBar.Width * 0.15f;
      Vector2 enemyHealthPositions = new(
        Position.X - scaleWidth * 0.5f,
        Position.Y - Collider.Height
      );
      spriteBatch.Draw(texture: TextureStore.Instance.HealthBar,
        position: enemyHealthPositions,
        sourceRectangle: null,
        color: Color.DarkSlateGray,
        rotation: 0.0f,
        origin: Vector2.Zero,
        scale: 0.15f,
        effects: SpriteEffects.None,
        layerDepth: 0.0f
       );
      int enemyHealthVisible = (int) (TextureStore.Instance.HealthBar.Width * enemyHealthPercent);
      Rectangle enemyHpSource = new(0, 0, enemyHealthVisible, TextureStore.Instance.HealthBar.Height);

      spriteBatch.Draw(
        texture: TextureStore.Instance.HealthBar,
        position: enemyHealthPositions,
        sourceRectangle: enemyHpSource,
        color: Color.White,
        rotation: 0.0f,
        origin: Vector2.Zero,
        scale: 0.15f,
        effects: SpriteEffects.None,
        layerDepth: 0.0f
      );
    }
  }

  public virtual void Update(double deltaTime) {
    if (DamageFlashTimer > 0.0) {
      DamageFlashTimer -= deltaTime;
    }
    UpdateCollider();
    CurrentState?.Update(deltaTime);
    if (Position.X < 0.0f) {
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

  public bool IsDead() {
    return Health <= 0;
  }
}
