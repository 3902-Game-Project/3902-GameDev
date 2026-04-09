using GameProject.Collisions;
using GameProject.Interfaces;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Projectiles;

public class BulletDefault : IProjectile, ICollidable {
  private readonly Texture2D texture;
  private Rectangle sourceRectangle = new(8, 0, 7, 7);
  private Vector2 origin;
  private readonly float scale = 2f;
  private Vector2 direction;
  private readonly float velocity;
  private readonly float bulletLifetime;
  private float lifetimeCounter = 0f;
  private readonly int damage = 50; //damage from bullet, can be changed
  private readonly int enemyDamage = 20;
  public bool IsExpired { get; private set; }
  public bool IsPlayerShot { get; set; } = true;
  public Vector2 Position { get; private set; }
  public BoxCollider Collider { get; private set; }
  public IShape Shape => Collider;
  public Layer Layer { get; } = Layer.Projectiles;
  public Layer Mask { get; } = Layer.Environment | Layer.Enemies;
  public BulletDefault(Texture2D texture, Vector2 startPosition,
    Vector2 direction, float velocity, float bulletLifetime) {
    this.texture = texture;
    Position = startPosition;
    this.direction = direction;
    this.velocity = velocity;
    this.bulletLifetime = bulletLifetime;
    Collider = new BoxCollider(sourceRectangle.Width * scale, sourceRectangle.Height * scale, Position);
  }
  public void Expire() {
    IsExpired = true;
  }

  public Rectangle BoundingBox {
    get {
      int width = (int) (sourceRectangle.Width * scale);
      int height = (int) (sourceRectangle.Height * scale);
      int x = (int) Position.X - (width / 2);
      int y = (int) Position.Y - (height / 2);

      return new Rectangle(x, y, width, height);
    }
  }

  public void Draw(SpriteBatch spriteBatch) {
    origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);

    spriteBatch.Draw(
      texture,
      Position,
      sourceRectangle,
      Color.White,
      0f,
      origin,
      scale,
      SpriteEffects.None,
      0f
    );
  }

  public void Update(GameTime gameTime) {
    // Logic for updating the bullet's position and state
    Position += direction * velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
    Collider.Position = Position;
    lifetimeCounter += (float) gameTime.ElapsedGameTime.TotalSeconds;
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
      player.TakeDamage(enemyDamage);
      Expire();
    }
  }

}
