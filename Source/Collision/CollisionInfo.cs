using GameProject.CollisionResponse;
using GameProject.Interfaces;
using Microsoft.Xna.Framework;

namespace GameProject.Collisions;

internal class CollisionInfo {
  public ICollidable Collider { get; set; } = null;
  public CollisionSide Side { get; set; } = CollisionSide.None;
  public Vector2 Direction { get; set; } = Vector2.Zero;
  public float Overlap { get; set; } = 0f;

  public CollisionInfo(ICollidable collider, CollisionSide side, Vector2 direction, float overlap) {
    Collider = collider;
    Side = side;
    Direction = direction;
    Overlap = overlap;
  }

  public CollisionInfo() { }
}
