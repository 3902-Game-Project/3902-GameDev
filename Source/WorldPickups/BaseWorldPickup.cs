using GameProject.Collisions;
using GameProject.Interfaces;
using GameProject.PlayerSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.WorldPickups;

public abstract class BaseWorldPickup : IWorldPickup {

  public Vector2 Position { get; set; }
  public IShape Shape { get; }
  public Layer Layer { get; } = Layer.Pickups;
  public Layer Mask { get; } = Layer.Player;

  protected BaseWorldPickup(Vector2 position) {
    Position = position;
    Shape = new CircleCollider(10, Position);
  }

  public abstract void Draw(SpriteBatch spriteBatch);
  public abstract void Update(GameTime gameTime);

  public virtual void OnPickup(Player player) { }

  public virtual void OnCollision(CollisionInfo info) {
    if (info.Collider.Layer == Layer.Player && info.Collider as Player != null) {
      OnPickup(info.Collider as Player);
    }
  }
}
