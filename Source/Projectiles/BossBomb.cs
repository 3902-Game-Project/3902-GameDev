using System.Collections.Generic;
using GameProject.Collisions;
using GameProject.Collisions.Shapes;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Projectiles;

internal class BossBomb : IProjectile, ICollidable {
  private const float THROW_SPEED = 150f;
  private const float FRICTION = 0.92f;
  private const float STOP_VELOCITY_THRESHOLD = 10f;
  private const double FUSE_TIME = 2.0;
  private const int NORMAL_HITBOX_SIZE = 40;
  private const int EXPLOSION_HITBOX_SIZE = 120;
  private const double NORMAL_ANIM_INTERVAL = 0.15;
  private const double EXPLOSION_ANIM_INTERVAL = 0.05;
  private const int EXPLOSION_START_FRAME = 12;
  public Vector2 Position { get; set; }
  public Vector2 Velocity { get; set; }

  private readonly Texture2D flyingTexture;
  private readonly Texture2D blinkingTexture;
  private int currentFrame = 0;
  private double animationTimer = 0;

  private readonly List<Rectangle> flyingRectangles;
  private readonly List<Rectangle> blinkingRectangles;

  private bool isLanded = false;
  private bool isExploding = false;
  private double fuseTimer = 0;
  private readonly int damage;

  public bool IsExpired { get; private set; } = false;
  public void Expire() { IsExpired = true; }
  public Rectangle BoundingBox => new(
    (int) Position.X - (NORMAL_HITBOX_SIZE / 2),
    (int) Position.Y - (NORMAL_HITBOX_SIZE / 2),
    NORMAL_HITBOX_SIZE,
    NORMAL_HITBOX_SIZE
   );

  public BoxCollider Collider { get; private set; }
  public IShape Shape => Collider;
  public Layer Layer { get; } = Layer.Projectiles;
  public Layer Mask { get; } = Layer.Player;

  public BossBomb(Texture2D flyingTex, Texture2D blinkingTex, Vector2 startPos, Vector2 direction, int damage) {
    flyingTexture = flyingTex;
    blinkingTexture = blinkingTex;
    Position = startPos;
    this.damage = damage;
    Velocity = direction * THROW_SPEED;
    Collider = new BoxCollider(NORMAL_HITBOX_SIZE, NORMAL_HITBOX_SIZE, Position);

    flyingRectangles = [
        new(527, 324, 31, 19),
        new(566, 324, 50, 19)
    ];

    blinkingRectangles = [
        new(7, 39, 40, 52), new(65, 43, 40, 48), new(119, 41, 41, 50),
        new(175, 43, 41, 48), new(231, 41, 41, 50), new(287, 38, 41, 53),
        new(344, 37, 41, 54), new(401, 37, 40, 55), new(456, 43, 41, 48),
        new(513, 38, 40, 55), new(568, 40, 41, 53), new(624, 44, 41, 49),

        new(29, 166, 56, 60), new(131, 158, 76, 73), new(234, 158, 98, 81),
        new(351, 146, 95, 97), new(457, 147, 101, 96), new(567, 143, 103, 105),
        new(15, 267, 82, 92), new(119, 260, 101, 100), new(235, 264, 93, 93)
    ];
  }

  public void Update(double deltaTime) {
    if (IsExpired) return;

    animationTimer += deltaTime;
    Collider.Position = Position;

    if (!isLanded) {
      Position += Velocity * (float) deltaTime;
      Velocity *= FRICTION;

      if (animationTimer > NORMAL_ANIM_INTERVAL) {
        currentFrame = (currentFrame + 1) % flyingRectangles.Count;
        animationTimer = 0;
      }

      if (Velocity.Length() < STOP_VELOCITY_THRESHOLD) {
        Velocity = Vector2.Zero;
        isLanded = true;
        currentFrame = 0;
        animationTimer = 0;
      }
    } else if (!isExploding) {
      fuseTimer += deltaTime;

      if (animationTimer > NORMAL_ANIM_INTERVAL) {
        currentFrame = (currentFrame + 1) % EXPLOSION_START_FRAME;
        animationTimer = 0;
      }

      if (fuseTimer > FUSE_TIME) {
        isExploding = true;
        currentFrame = EXPLOSION_START_FRAME;
        animationTimer = 0;
        Collider = new BoxCollider(EXPLOSION_HITBOX_SIZE, EXPLOSION_HITBOX_SIZE, Position);
      }
    } else {
      if (animationTimer > EXPLOSION_ANIM_INTERVAL) {
        currentFrame++;
        animationTimer = 0;

        if (currentFrame >= blinkingRectangles.Count) {
          Expire();
        }
      }
    }
  }

  public void OnCollision(CollisionInfo collisionInfo) {
    if (isExploding && collisionInfo.Collider is Player player) {
      player.TakeDamage(damage);
    }
  }

  public void Draw(SpriteBatch spriteBatch) {
    if (IsExpired) return;

    Texture2D currentTexture = isLanded ? blinkingTexture : flyingTexture;
    Rectangle sourceRect = isLanded ? blinkingRectangles[currentFrame] : flyingRectangles[currentFrame];

    Vector2 origin = new(sourceRect.Width / 2f, sourceRect.Height / 2f);

    spriteBatch.Draw(currentTexture, Position, sourceRect, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0f);
  }
}
