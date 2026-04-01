using System.Collections.Generic;
using GameProject.CollisionResponse;
using GameProject.Collisions;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

public abstract class BaseEnemy(Texture2D texture, Vector2 position, float colliderWidth = 64f, float colliderHeight = 64f) : IEnemy {
  public Texture2D Texture { get; protected set; } = texture;
  public Vector2 Position { get; set; } = position;
  public Vector2 Velocity { get; set; }
  public int FacingDirection { get; set; } = 1;

  public List<Rectangle> CurrentSourceRectangles;
  public int CurrentFrame;
  public IShape Shape => Collider;
  public BoxCollider Collider { get; private set; } = new BoxCollider(colliderWidth, colliderHeight, position);
  public Layer Layer { get; } = Layer.Enemies;
  public Layer Mask { get; } = Layer.Player;
  public int Health { get; set; } = 100;
  public int MaxHealth { get; set; } = 100;
  public float DamageFlashTimer { get; protected set; }
  protected const float DamageFlashDuration = 0.15f;

  public Rectangle BoundingBox => throw new System.NotImplementedException();

  protected void UpdateCollider() {
    if (Collider != null) {
      Collider.position = Position + new Vector2(0, -Collider.height / 2f);
    }
  }

  public virtual void OnCollision(CollisionInfo info) {
    if (info.Collider is IBlock) {
      if (info.Side == CollisionSide.Left || info.Side == CollisionSide.Right) {
        Velocity = new Vector2(0, Velocity.Y);
        FacingDirection *= -1;
      } else if (info.Side == CollisionSide.Top || info.Side == CollisionSide.Bottom) {
        Velocity = new Vector2(Velocity.X, 0);
      }
      Position += info.Direction * 2f;
      UpdateCollider();
    }
  }
  public abstract void Draw(SpriteBatch spriteBatch);

  public virtual void Update(GameTime gameTime) {
    if (DamageFlashTimer > 0) {
      DamageFlashTimer -= (float) gameTime.ElapsedGameTime.TotalSeconds;
    }
    UpdateCollider();
  }

  public virtual void TakeDamage(int damage) {
    if (Health <= 0) return;

    Health -= damage;
    DamageFlashTimer = DamageFlashDuration;
  }
}
