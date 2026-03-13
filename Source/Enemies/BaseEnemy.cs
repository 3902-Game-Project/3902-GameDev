using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Interfaces;
using GameProject.CollisionResponse;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Enemies;

public abstract class BaseEnemy : IEnemy {
  public Texture2D Texture { get; protected set; }
  public Vector2 Position { get; set; }
  public Vector2 Velocity { get; set; }
  public int FacingDirection { get; set; } = 1;

  public List<Rectangle> CurrentSourceRectangles;
  public int CurrentFrame;
  public IShape Shape => Collider;
  public BoxCollider Collider { get; private set; }
  public Layer Layer { get; } = Layer.Enemies;
  public Layer Mask { get; } = Layer.Player;

  public Rectangle BoundingBox => throw new System.NotImplementedException();

  protected BaseEnemy(Texture2D texture, Vector2 position, float colliderWidth = 64f, float colliderHeight = 64f) {
    Texture = texture;
    Position = position;
    Collider = new BoxCollider(colliderWidth, colliderHeight, position);
  }

  protected void UpdateCollider() {
    if (Collider != null) {
      Collider.position = this.Position + new Vector2(0, -Collider.height / 2f);
    }
  }

  public virtual void OnCollision(CollisionInfo info) {
    if (info.Collider is IBlock block) {
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
  public abstract void Update(GameTime gameTime);
  public abstract void Draw(SpriteBatch spriteBatch);
  public abstract void TakeDamage();
}
