using GameProject.Blocks;
using GameProject.Collisions;
using GameProject.Collisions.Shapes;
using GameProject.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Projectiles;

internal class BFGProjectile(Texture2D texture, Vector2 position, Vector2 direction, float speed, int damage) : IProjectile, ICollidable {
  private static readonly Rectangle SOURCE_RECT = new(48, 0, 16, 16);

  private readonly BoxCollider boxCollider = new(16f, 16f, position);

  internal bool IsExpired { get; private set; }
  internal Vector2 Position { get; set; } = position;

  internal Rectangle BoundingBox => new((int) Position.X - 8, (int) Position.Y - 8, 16, 16);

  internal IShape Shape => boxCollider;
  internal Layer Layer { get; } = Layer.Projectiles;

  internal void Update(double deltaTime) {
    Position += direction * speed * (float) deltaTime;
    boxCollider.Position = Position;
  }

  internal void Draw(SpriteBatch spriteBatch) {
    spriteBatch.Draw(texture, Position, SOURCE_RECT, Color.White, 0f, new Vector2(8, 8), 2f, SpriteEffects.None, 0f);
  }

  internal void OnCollision(CollisionInfo info) {
    if (info.Collider is IEnemy enemy) {
      enemy.TakeDamage(damage);
      Expire();
    } else if (info.Collider is IBlock) {
      Expire();
    }
  }

  internal void Expire() => IsExpired = true;
}
