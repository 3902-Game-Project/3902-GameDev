using System;
using System.Collections.Generic;
using GameProject.Blocks;
using GameProject.Collisions;
using GameProject.Collisions.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

internal abstract class ABaseEnemy(Texture2D texture, Vector2 position, float colliderWidth = 64f, float colliderHeight = 64f) : IEnemy {
  public Texture2D Texture { get; protected set; } = texture;
  public Vector2 Position { get; set; } = position;
  public Vector2 Velocity { get; set; }
  public FacingDirection Direction { get; set; } = FacingDirection.Right;

  public Vector2 Target { get; set; } = position;

  public static event Action<ABaseEnemy> OnDeath;

  public List<Rectangle> CurrentSourceRectangles { get; set; } = [];
  public int CurrentFrame { get; set; }
  public IShape Shape => Collider;
  public BoxCollider Collider { get; private set; } = new BoxCollider(colliderWidth, colliderHeight, position);
  public Layer Layer { get; } = Layer.Enemies;
  public Layer Mask { get; } = Layer.Player;
  public int Health { get; set; } = 100;
  public int MaxHealth { get; set; } = 100;
  public float DamageFlashTimer { get; protected set; }
  protected const float DamageFlashDuration = 0.15f;
  protected virtual void DropLoot() { }
  protected virtual void TransitionToDeathState() { }

  public Rectangle BoundingBox => new((int) (Position.X - Collider.Width / 2), (int) (Position.Y - Collider.Height / 2), (int) Collider.Width, (int) Collider.Height);
  public IEnemyState CurrentState { get; set; }
  protected float DrawScale { get; set; } = 1f;
  protected bool FlipOnRightDir { get; set; } = true;

  protected void UpdateCollider() {
    if (Collider != null) {
      Collider.Position = Position + new Vector2(0, -Collider.Height / 2f);
    }
  }

  public virtual void OnCollision(CollisionInfo info) {
    if (info.Collider is IBlock) {
      if (info.Side == CollisionSide.Left || info.Side == CollisionSide.Right) {
        Velocity = new Vector2(0, Velocity.Y);
        Direction = (info.Side == CollisionSide.Left) ? FacingDirection.Left : FacingDirection.Right;
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

  public virtual void Update(GameTime gameTime) {
    if (DamageFlashTimer > 0) {
      DamageFlashTimer -= (float) gameTime.ElapsedGameTime.TotalSeconds;
    }
    UpdateCollider();
    CurrentState?.Update(gameTime);
    if (Position.X < 0) {
      Position = new Vector2(0, Position.Y);
    }
  }

  public virtual void TakeDamage(int damage) {
    if (Health <= 0) return;
    bool wasAlive = Health > 0;
    Health -= damage;
    DamageFlashTimer = DamageFlashDuration;

    if (wasAlive && Health <= 0) {
      Die();
    }
  }

  public virtual void FollowTarget(float speed) {
    Vector2 direction = Target - Position;
    if (direction != Vector2.Zero) {
      direction = Vector2.Normalize(direction);
    }
    Velocity = direction * speed;
    Direction = (direction.X > 0) ? FacingDirection.Right : FacingDirection.Left;
  }

  public virtual void Die() {
    DropLoot();
    OnDeath?.Invoke(this);
    TransitionToDeathState();
  }
}
