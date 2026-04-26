using GameProject.Collisions;
using GameProject.Collisions.Shapes;
using GameProject.Items;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Projectiles;

internal class BangProjectile : IProjectile, ICollidable {
  public bool IsExpired { get; private set; }
  public Vector2 Position { get; set; }
  public Rectangle BoundingBox => new Rectangle((int) Position.X, (int) Position.Y, 1, 1);

  public IShape Shape { get; }
  public Layer Layer { get; } = Layer.Projectiles;
  public Layer Mask { get; } = Layer.Environment;

  private readonly Texture2D texture;
  private readonly ABaseGun sourceGun;
  private FacingDirection direction;
  private readonly Rectangle sourceRect = new(30, 20, 30, 20);
  private double lifetime = 1.0;

  public BangProjectile(Texture2D texture, ABaseGun sourceGun) {
    this.texture = texture;
    this.sourceGun = sourceGun;
    this.direction = sourceGun.Direction;
    this.Position = sourceGun.Position;
    this.Shape = new BoxCollider(1f, 1f, Position);
  }

  public void Update(double deltaTime) {
    lifetime -= deltaTime;
    if (lifetime <= 0) Expire();

    direction = sourceGun.Direction;

    Vector2 barrelOffset = Vector2.Zero;
    if (direction == FacingDirection.Left) barrelOffset = new Vector2(-20, 0);
    else if (direction == FacingDirection.Right) barrelOffset = new Vector2(20, 0);
    else if (direction == FacingDirection.Up) barrelOffset = new Vector2(0, -20);
    else if (direction == FacingDirection.Down) barrelOffset = new Vector2(0, 20);

    Vector2 visualNudge = new Vector2(-4, 8);

    Position = sourceGun.Position + barrelOffset + visualNudge;
  }

  public void Draw(SpriteBatch spriteBatch) {
    SpriteEffects effects = SpriteEffects.None;
    float rotation = 0f;

    // Match the rotation logic of the ABaseGun
    if (direction == FacingDirection.Left) {
      effects = SpriteEffects.FlipHorizontally;
    } else if (direction == FacingDirection.Up) {
      rotation = -MathHelper.PiOver2; // Rotates 90 degrees counter-clockwise
    } else if (direction == FacingDirection.Down) {
      rotation = MathHelper.PiOver2;  // Rotates 90 degrees clockwise
    }

    // Origin remains at the attachment point (the "stick" of the flag)
    Vector2 origin = direction == FacingDirection.Left ? new Vector2(30, 10) : new Vector2(0, 10);

    // Ensure the rotation variable is passed to the Draw call
    spriteBatch.Draw(texture, Position, sourceRect, Color.White, rotation, origin, 1f, effects, 0f);
  }

  public void OnCollision(CollisionInfo info) { }
  public void Expire() => IsExpired = true;
}
