using GameProject.Collisions;
using GameProject.Collisions.Shapes;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.WorldPickups;

internal abstract class ABaseWorldPickup : IWorldPickup {
  internal Vector2 Position { get; set; }
  internal virtual bool IsAutoCollect => false;
  internal IShape Shape { get; }
  internal Layer Layer { get; } = Layer.Pickups;

  private protected ABaseWorldPickup(Vector2 position) {
    Position = position;
    Shape = new CircleCollider(10, Position);
  }

  internal abstract void Update(double deltaTime);

  internal abstract void Draw(SpriteBatch spriteBatch);

  internal virtual void OnPickup(Player player) { }

  internal virtual void OnCollision(CollisionInfo info) {
    if (info.Collider.Layer == Layer.Player && info.Collider as Player != null) {
      OnPickup(info.Collider as Player);
    }
  }
}
