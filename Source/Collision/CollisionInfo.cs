using GameProject.Interfaces;
using GameProject.Source.CollisionResponse;
using Microsoft.Xna.Framework;

namespace GameProject.Collisions;

public class CollisionInfo {
  public ICollidable Collider { get; set; } = null;
  public CollisionSide Side { get; set; } = CollisionSide.None;
  public Vector2 Direction { get; set; } = Vector2.Zero;

  public CollisionInfo(ICollidable collider, CollisionSide side, Vector2 direction) {
    Collider = collider;
    Side = side;
    Direction = direction;
  }

  public CollisionInfo() { }
}
