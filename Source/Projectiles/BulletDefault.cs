using GameProject.Blocks;
using GameProject.Collisions;
using GameProject.Collisions.Shapes;
using GameProject.Enemies;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Projectiles;

internal class BulletDefault : IProjectile, ICollidable {
  private static readonly Rectangle SOURCE_RECT = new(8, 0, 7, 7);
  private static readonly float SCALE = 2.0f;
  private static readonly int ENEMY_DAMAGE = 20;

  private readonly Texture2D texture;
  private Vector2 origin;
  private Vector2 direction;
  private readonly float velocity;
  private readonly float bulletLifetime;
  private float lifetimeCounter = 0f;
  private readonly int damage;

  public bool IsExpired { get; private set; }
  public bool IsPlayerShot { get; set; } = true;
  public Vector2 Position { get; private set; }
  public BoxCollider Collider { get; private set; }
  public IShape Shape => Collider;
  public Layer Layer { get; } = Layer.Projectiles;
  public Layer Mask { get; } = Layer.Environment | Layer.Enemies;

  public BulletDefault(Texture2D texture, Vector2 startPosition,
    Vector2 direction, float velocity, float bulletLifetime, int damage) {
    this.texture = texture;
    Position = startPosition;
    this.direction = direction;
    this.velocity = velocity;
    this.bulletLifetime = bulletLifetime;
    this.damage = damage;
    Collider = new BoxCollider(SOURCE_RECT.Width * SCALE, SOURCE_RECT.Height * SCALE, Position);
  }

  public void Expire() {
    IsExpired = true;
  }

  public Rectangle BoundingBox {
    get {
      int width = (int) (SOURCE_RECT.Width * SCALE);
      int height = (int) (SOURCE_RECT.Height * SCALE);
      int x = (int) Position.X - (width / 2);
      int y = (int) Position.Y - (height / 2);

      return new Rectangle(x, y, width, height);
    }
  }

  public void Draw(SpriteBatch spriteBatch) {
    origin = new Vector2(SOURCE_RECT.Width / 2, SOURCE_RECT.Height / 2);

    spriteBatch.Draw(
      texture,
      Position,
      SOURCE_RECT,
      Color.White,
      0f,
      origin,
      SCALE,
      SpriteEffects.None,
      0f
    );
  }

  public void Update(double deltaTime) {
    // Logic for updating the bullet's position and state
    Position += direction * velocity * ((float) deltaTime);
    Collider.Position = Position;
    lifetimeCounter += (float) deltaTime;
    if (lifetimeCounter >= bulletLifetime) {
      // Logic for destroying the bullet
      Expire();
    }
  }

  public void OnCollision(CollisionInfo collisionInfo) {
    if (collisionInfo.Collider is IBlock) {
      Expire();
    } else if (collisionInfo.Collider is IEnemy enemy && IsPlayerShot) {
      enemy.TakeDamage(damage);
      Expire();
    } else if (collisionInfo.Collider is Player player && !IsPlayerShot) {
      player.TakeDamage(ENEMY_DAMAGE);
      Expire();
    }
  }
}
