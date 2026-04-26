using GameProject.Blocks;
using GameProject.Collisions;
using GameProject.Collisions.Shapes;
using GameProject.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Projectiles;

internal class BFGProjectile(Texture2D texture, Vector2 position, Vector2 direction, float speed, int damage) : IProjectile, ICollidable {
  public bool IsExpired { get; private set; }
  public Vector2 Position { get; set; } = position;

  public Rectangle BoundingBox => new((int) Position.X - 8, (int) Position.Y - 8, 16, 16);

  private readonly BoxCollider boxCollider = new(16f, 16f, position);
  public IShape Shape => boxCollider;
  public Layer Layer { get; } = Layer.Projectiles;
  public Layer Mask { get; } = Layer.Environment | Layer.Enemies;

  private readonly Rectangle sourceRect = new(48, 0, 16, 16);

  public void Update(double deltaTime) {
    Position += direction * speed * (float) deltaTime;
    boxCollider.Position = Position;
  }

  public void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, sourceRect, Color.White, 0f, new Vector2(8, 8), 2f, SpriteEffects.None, 0f);
  }

  public void OnCollision(CollisionInfo info) {
    if (info.Collider is IEnemy enemy) {
      enemy.TakeDamage(damage);
      Expire();
    } else if (info.Collider is IBlock) {
      Expire();
    }
  }

  public void Expire() => IsExpired = true;
}
