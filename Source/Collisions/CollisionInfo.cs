using Microsoft.Xna.Framework;

namespace GameProject.Collisions;

internal class CollisionInfo {
  internal ICollidable Collider { get; set; } = null;
  internal CollisionSide Side { get; set; } = CollisionSide.None;
  internal Vector2 Direction { get; set; } = Vector2.Zero;
  internal float Overlap { get; set; } = 0f;

  internal CollisionInfo(ICollidable collider, CollisionSide side, Vector2 direction, float overlap) {
    Collider = collider;
    Side = side;
    Direction = direction;
    Overlap = overlap;
  }

  internal CollisionInfo() { }
}
