using GameProject.Collisions;
using GameProject.Collisions.Shapes;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.WorldPickups;

internal abstract class ABaseWorldPickup : IWorldPickup {
  public Vector2 Position { get; set; }
  public virtual bool IsAutoCollect => false;
  public IShape Shape { get; }
  public Layer Layer { get; } = Layer.Pickups;
  public Layer Mask { get; } = Layer.Player;

  protected ABaseWorldPickup(Vector2 position) {
    Position = position;
    Shape = new CircleCollider(10, Position);
  }

  public abstract void Update(double deltaTime);

  public abstract void Draw(SpriteBatch spriteBatch);

  public virtual void OnPickup(Player player) { }

  public virtual void OnCollision(CollisionInfo info) {
    if (info.Collider.Layer == Layer.Player && info.Collider as Player != null) {
      OnPickup(info.Collider as Player);
    }
  }
}
